using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Requests;
using VTubeStudioAPI.Responses;
using WebSocketSharp;

namespace VTubeStudioAPI;
internal class VTubeStudioImpl : IVTubeStudio
{
    private const int BufferSize = 1024 * 1024;
    private ILogger<VTubeStudioImpl> Logger { get; }
    public IAuthManager AuthManager { get; }
    public IVTubeStudioSettings Settings { get; }
    private WebSocket? WebSocket { get; set; }
    public bool IsAuthed => Authed && (WebSocket?.IsAlive ?? false);
    public bool Authed { get; private set; } = false;
    public bool Connecting { get; private set; } = false;

    private readonly CancellationTokenSource CancellationTokenSource = new();
    private readonly SemaphoreSlim ConnectionSemaphore = new(1);

    public VTubeStudioImpl(
        ILogger<VTubeStudioImpl> logger,
        IAuthManager authManager,
        IVTubeStudioSettings settings
    )
    {
        Logger = logger;
        AuthManager = authManager;
        Settings = settings;
    }

    public async void Run()
    {

    }

    public void Stop() => CancellationTokenSource.Cancel();

    public async void EnsureConnected()
    {
        if (ConnectionSemaphore.CurrentCount == 0) return;
        await ConnectionSemaphore.WaitAsync();

        try
        {
            if (WebSocket?.IsAlive ?? false)
                return;

            Connect();
        }
        finally
        {
            ConnectionSemaphore.Release();
        }
    }

    private void Connect()
    {
        Logger.LogDebug("Connect called");
        if (!AuthManager.AllowConnect)
        {
            Logger.LogDebug("Connection stopped by setting");
            return;
        }

        Authed = false;
        if (Settings.Host == null || Settings.Port == null)
            return;

        var uri = new UriBuilder() { Host = Settings.Host, Port = Settings.Port.Value, Scheme = "ws" }.Uri;
        Logger.LogInformation("Connecting to {Uri}", uri);
        WebSocket = new(uri.ToString());
        SetupEvents();
        WebSocket.Connect();
    }

    private void SetupEvents()
    {
        if (WebSocket is null) throw new ArgumentException(nameof(WebSocket));

        WebSocket.OnOpen += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(AuthManager.Token))
                Send(new AuthWithTokenRequest(AuthManager.Token));
            else
                Send(new AuthenticateRequest());
            VTSEvents.TriggerSocketConnected(this);
        };

        WebSocket.OnMessage += MessageReceived;
        WebSocket.OnError += (sender, e) => Logger.LogInformation("Error from WebSocket: {Error}", e.Message);

        WebSocket.OnClose += (sender, args) => {
            Logger.LogInformation("Disconnected from WebSocket: ({Code}) {Reason}", args.Code, args.Reason);
            VTSEvents.TriggerSocketClosed(this, args);
        };
    }

    public void Reconnect()
    {
        Disconnect();
        Connect();
    }

    private void Disconnect()
    {
        if (WebSocket is null) return;

        try
        {
            WebSocket.Close();
            WebSocket = null;
        }
        catch
        {
            //ignore
        }
    }

    public void Send(ApiRequest request, string? requestId = null)
    {
        if (WebSocket is null) return;

        var data = JsonConvert.SerializeObject(new RequestWrapper(request) { RequestId = requestId });
        Logger.LogDebug("Sending message: {Json}", data);
        WebSocket.Send(data);
    }

    public void SetConnectionParams(string host, ushort port)
    {
        if (!Settings.SetConnectionParams(host, port)) return;

        Logger.LogInformation("New connection details, set to: ws://{IpAddress}:{Port}", host, port);

        Connect(); 
    }

    private void MessageReceived(object? sender, MessageEventArgs e)
    {
        if (!e.IsText) return;

        Logger.LogTrace("Got JSON data {Json}", e.Data);

        var response = JsonConvert.DeserializeObject<ApiResponse>(e.Data, new VtubeStudioMapping());

        if (response is null) return;

        switch (response)
        {
            case ApiResponse<ApiErrorResponse> m:
                VTSEvents.TriggerOnApiError(this, new(m.Data!.Id, m.RequestId));
                break;
            
            case ApiResponse<ApiStateResponse> m:
                Authed = m.Data?.Authenticated ?? false;
                VTSEvents.TriggerOnApiState(this, new(m.Data, m.RequestId));
                break;
            
            case ApiResponse<AuthenticateResponse> m:
                AuthManager.Token = m.Data!.AuthToken;
                Send(new AuthWithTokenRequest(m.Data!.AuthToken));
                break;

            case ApiResponse<AuthenticationResponse> m:
                Authed = m.Data?.Authenticated ?? false;
                if (!Authed)
                {
                    AuthManager.Token = null;
                    Send(new AuthenticateRequest());
                }
                

                VTSEvents.TriggerOnAuthenticationResponse(this, new(m.Data, m.RequestId));
                break;

            case ApiResponse<CurrentModelResponse> m:
                VTSEvents.TriggerOnCurrentModelInformation(this, new(m.Data, m.RequestId));
                break;

            case ApiResponse<AvailableModelsResponse> m:
                VTSEvents.TriggerOnAvailableModels(this, new(m.Data, m.RequestId));
                break;

            case ApiResponse<ModelLoadResponse> m:
                VTSEvents.TriggerOnModelLoad(this, new(m.Data, m.RequestId));
                break;

            case ApiResponse<MoveModelResponse> m:
                VTSEvents.TriggerOnModelMove(this, new(m.Data, m.RequestId));
                break;

            case ApiResponse<ModelHotkeysResponse> m:
                VTSEvents.TriggerOnModelHotkeys(this, new(m.Data, m.RequestId));
                break;

            case ApiResponse<HotkeyTriggerResponse> m:
                VTSEvents.TriggerOnHotkeyTriggered(this, new(m.Data, m.RequestId));
                break;

            default: return;
        }
    }
}

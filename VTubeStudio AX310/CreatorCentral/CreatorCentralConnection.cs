using AverMediaVTubeStudio.CreatorCentral.Messages;
using AverMediaVTubeStudio.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Plugin.Contracts;
using Plugin.Contracts.Actions;
using Plugin.Contracts.Requests;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace AverMediaVTubeStudio.CreatorCentral;
internal class CreatorCentralConnection : ICreatorCentralConnection
{
    private const int BufferSize = 1024 * 1024;
    private readonly IRequestFactory requestFactory;
    private readonly IActionRepository actionRepository;
    private ClientWebSocket? websocket;
    private CancellationTokenSource cancellationTokenSource = new();
    private SemaphoreSlim sendSemaphore = new SemaphoreSlim(1);
    public RegistrationOptions RegistrationOptions { get; }
    public ILogger<CreatorCentralConnection> Logger { get; }
    public IEnumerable<IGlobalSettingsHandler> GlobalSettingsHandlers { get; }

    public CreatorCentralConnection(
        IOptions<RegistrationOptions> registrationOptions,
        ILogger<CreatorCentralConnection> logger,
        IEnumerable<IGlobalSettingsHandler> globalSettingsHandlers,
        IRequestFactory requestFactory,
        IActionRepository actionRepository
    )
    {
        RegistrationOptions = registrationOptions.Value;
        Logger = logger;
        GlobalSettingsHandlers = globalSettingsHandlers;
        this.requestFactory = requestFactory;
        this.actionRepository = actionRepository;
    }

    public async Task Cancel()
    {
        if (websocket is null) return;

        if (websocket.State == WebSocketState.Open)
            await websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);

        cancellationTokenSource.Cancel();
    }

    public async Task Run()
    {
        websocket = new ClientWebSocket();
        await websocket.ConnectAsync(new($"ws://localhost:{RegistrationOptions.Port}"), CancellationToken.None);

        while (websocket.State == WebSocketState.Connecting) Thread.Sleep(100); //wait until connected.

        if (websocket.State != WebSocketState.Open)
        {
            Logger.LogError("Unable to connect to websocket... current state {CurrentState}", websocket.State);
            throw new Exception();
        }

        await SendMessage(new RegisterEvent
        {
            Event = this.RegistrationOptions.RegisterEvent,
            Uuid = this.RegistrationOptions.Uuid,
        });

        await SendMessage(requestFactory.CreateRequest<IRequestGlobalSettings>());

        ReceiveAsync();
    }

    private async void ReceiveAsync()
    {
        if (websocket is null) return;

        var buffer = new byte[BufferSize];
        var arrayBuffer = new ArraySegment<byte>(buffer);
        var textBuffer = new StringBuilder();

        while (!cancellationTokenSource.IsCancellationRequested)
        {
            var result = await websocket.ReceiveAsync(arrayBuffer, cancellationTokenSource.Token);

            if (result.MessageType == WebSocketMessageType.Close || (result.CloseStatus is { } && result.CloseStatus.Value != WebSocketCloseStatus.Empty))
            {
                Logger.LogInformation("Received disconnect event, of type {CloseStatus} reason given? {CloseStatusDescription}", result.CloseStatus, result.CloseStatusDescription);

                return;
            }

            if (result.MessageType != WebSocketMessageType.Text) continue; //ignore all non-text

            textBuffer.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
            if (!result.EndOfMessage) continue;

            Logger.LogTrace("Got JSON: {Json}", textBuffer.ToString());

            var message = JsonConvert.DeserializeObject<BaseMessage>(textBuffer.ToString(), new CreatorCentralEventConverter());
            
            if (message is null)
            {
                Logger.LogCritical("Failed to decode message: {json}", textBuffer.ToString());
                textBuffer.Clear();
                continue;
            }

            switch (message)
            {
                case DidReceiveWidgetSettings m:
                    actionRepository.GotSettings(m);
                    break;

                case DidRecievePackageSettings m:
                    foreach (var handler in GlobalSettingsHandlers)
                        handler.GotGlobalSettings(m);
                    break;

                case IKeyDown m:
                    actionRepository.ButtonDown(m);
                    break;

                case IKeyUp m:
                    actionRepository.ButtonUp(m);
                    break;

                case IWidgetAppeared m:
                    actionRepository.Appeared(m);
                    break;

                case IWidgetDisppeared m:
                    actionRepository.Disappeared(m);
                    break;

                case IPropertyViewAppear m:
                    actionRepository.PropertyInspectorAppeared(m);
                    break;

                case IPropertyViewDisappear m:
                    actionRepository.PropertyInspectorDisappeared(m);
                    break;

                case ISendToPlugin m:
                    actionRepository.SendToPlugin(m);
                    break;

                default:
                    Logger.LogDebug("Unhandled message of type {Type}", message.GetType());
                    break;
            }

            textBuffer.Clear();
        }
    }

    public async Task SendMessage(IMessage? message)
    {
        if (message is null) return; //unhandled message type according to mappings - ignore.

        if (websocket is null || websocket.State != WebSocketState.Open) throw new Exception();

        try
        {
            await sendSemaphore.WaitAsync();

            var json = JsonConvert.SerializeObject(
                message,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                }
            );
            Logger.LogDebug("Sending message with JSON data {Json}", json);
            var buf = Encoding.UTF8.GetBytes(json);
            await websocket.SendAsync(new ArraySegment<byte>(buf), WebSocketMessageType.Text, true, cancellationTokenSource.Token);
        }
        finally
        {
            sendSemaphore.Release();
        }
    }
}

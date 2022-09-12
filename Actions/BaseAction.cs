using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Plugin.Contracts;
using Plugin.Contracts.Actions;
using Plugin.Contracts.Requests;
using System.Reflection;
using VTubeStudioAPI.Contracts;

namespace Actions;

public abstract class BaseAction<T> : Plugin.Actions.BaseAction<T>, IPropertyInspector, ITickHandler where T : new()
{
    protected IVTubeStudio Vts;
    private readonly Dictionary<string, MethodInfo> _commands = new();
    protected readonly IGlobalSettingsHandler Gsm;
    private readonly ILogger _logger;

    public BaseAction(IGlobalSettingsHandler gsm, IVTubeStudio vts, IConnection isd, ILogger logger, IRequestFactory requestFactory) : base(isd, requestFactory)
    {
        Gsm = gsm;
        Vts = vts;
        _logger = logger;
        Vts.EnsureConnected();
        LoadCommands();
    }

    private void LoadCommands()
    {
        foreach (var methodInfo in this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
        {
            var attribute = methodInfo.GetCustomAttribute<PluginCommandAttribute>();
            if (attribute is null)
                continue;

            _commands.Add(attribute.Command.ToLower(), methodInfo);
        }
    }

    public override void KeyDown(IKeyDown keyActionPayload)
    {
        Vts.EnsureConnected();
        if (!Vts.IsAuthed)
        {
            ShowAlert();
            return;
        }

        Pressed();
    }

    public override void KeyUp(IKeyUp keyActionPayload) => Released();

    protected async Task UpdateClient()
    {
        await Connection.SendMessage(this.RequestFactory.CreateRequest<ISendToPropertyView>(s =>
        {
            s.ContextId = ContextId!;
            s.Payload = GetClientData();
        }));
    }

    [PluginCommand("refresh")]
    public virtual async void Refresh(PluginPayload pl) => await UpdateClient();

    [PluginCommand("force-reconnect")]
    public void ForceReconnect(PluginPayload pl) => Vts.Reconnect();

    [PluginCommand("set-vtsinfo")]
    public void SetVtsInfo(PluginPayload pl)
    {
        var (host, port) = ((string)(pl.Payload?["host"] ?? "127.0.0.1"), (ushort)(pl.Payload?["port"] ?? 8001));

        Vts.SetConnectionParams(host, port);
    }

    protected abstract void Pressed();
    protected abstract void Released();
    protected abstract object? GetClientData();
    protected abstract void SettingsUpdated(T oldSettings, T newSettings);

    public async void Appeared(IPropertyViewAppear didAppear)
    {
        await UpdateClient();
    }

    public void Disappeared(IPropertyViewDisappear didDisappear)
    {
    }

    public void OnSendToPlugin(ISendToPlugin sendToPlugin)
    {
        var pl = sendToPlugin.GetPayload<PluginPayload>();
        if (pl?.Command == null)
        {
            return;
        }

        var command = pl.Command.ToLower();
        if (!_commands.ContainsKey(command))
            return;

        _commands[command].Invoke(this, new object[] { pl });
    }

    public override void GotSettings(JObject? settings)
    {
        var oldSettings = Settings;
        base.GotSettings(settings);

        SettingsUpdated(oldSettings, Settings);
    }

    public virtual async void Tick()
    {
        Vts.EnsureConnected();
        await UpdateClient();
    }
}

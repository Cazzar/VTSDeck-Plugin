using Plugin.Contracts;
using Plugin.Contracts.Requests;
using VTubeStudioAPI.Contracts;

namespace Plugin;

public class AuthManager : IAuthManager, IGlobalSettingsHandler, IVTubeStudioSettings
{
    private readonly Lazy<IConnection> connection;
    private readonly IRequestFactory requestFactory;

    public GlobalSettings Settings { get; private set; } = new();
    public string? Token
    {
        get => Settings.Token;
        set
        {
            Settings.Token = value;
            SaveSettings();
        }
    }

    public string Host { get => Settings.Host; set => Settings.Host = value; }
    public ushort? Port { get => Settings.Port; set => Settings.Port = value; }

    public AuthManager(Lazy<IConnection> connection, IRequestFactory requestFactory)
    {
        this.connection = connection;
        this.requestFactory = requestFactory;
    }

    public void GotGlobalSettings(IGlobalSettings settings) => Settings = settings.GetSettings<GlobalSettings>()!;

    private void SaveSettings() => connection.Value.SendMessage(requestFactory.CreateRequest<ISaveGlobalSettings>(s => s.Settings = Settings));

    bool IVTubeStudioSettings.SetConnectionParams(string host, ushort port)
    {
        if ((host, port) == (Host, Port)) return false;

        (Host, Port) = (host, port);
        SaveSettings();
        return true;
    }
}

public class GlobalSettings
{
    public string? Token { get; set; } = string.Empty;
    public string Host { get; set; } = "127.0.0.1";
    public ushort? Port { get; set; } = 8001;
}
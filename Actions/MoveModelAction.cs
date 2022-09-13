using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plugin;
using Plugin.Contracts;
using Plugin.Contracts.Requests;
using VTubeStudioAPI;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Requests;
using VTubeStudioAPI.Responses;
using IRequestFactory = Plugin.Contracts.Requests.IRequestFactory;

namespace Actions;
[CustomAction("dev.cazzar.vtubestudio.movemodel")]
class MoveModelAction : BaseAction<MoveModelAction.PluginSettings>, IDisposable
{
    private readonly ILogger<MoveModelAction> _logger;

    public class PluginSettings
    {
        [JsonProperty("seconds")]
        public double? Seconds { get; set; } = 0;

        [JsonProperty("posX")]
        public double? PosX { get; set; } = 0;

        [JsonProperty("posY")]
        public double? PosY { get; set; } = 0;

        [JsonProperty("rotation")]
        public double? Rotation { get; set; } = 0;

        [JsonProperty("size")]
        public double? Size { get; set; } = null;

        [JsonProperty("relative")]
        public bool Relative { get; set; } = true;
    }

    private string _requestId = string.Empty;

    // public MoveModelAction(GlobalSettingsManager gsm, VTubeStudioWebsocketClient vts) : base(gsm, vts)
    public MoveModelAction(
        IGlobalSettingsHandler gsm,
        IVTubeStudio vts,
        IConnection isd,
        ILogger<MoveModelAction> logger,
        IRequestFactory requestFactory
    ) : base(gsm, vts, isd, logger, requestFactory)
    {
        _logger = logger;
        VTSEvents.OnCurrentModelInformation += CurrentModelInformation;
    }

    private void CurrentModelInformation(object? sender, ApiEventArgs<CurrentModelResponse> e)
    {
        if (string.IsNullOrEmpty(e.RequestId)) return; //I don't care about this request.
        if (_requestId != e.RequestId) return;

        var pos = e.Response!.ModelPosition;
        Settings.PosX = pos.X;
        Settings.PosY = pos.Y;
        Settings.Rotation = pos.Rotation;
        Settings.Size = pos.Size;
        Settings.Relative = false;

        SaveSettings();
    }

    protected override void Pressed()
    {
        Vts.Send(new MoveModelRequest()
        {
            PositionX = Settings.PosX,
            PositionY = Settings.PosY,
            RelativeMove = Settings.Relative,
            Rotation = Settings.Rotation,
            Size = Settings.Size,
            TimeInSeconds = Settings.Seconds,
        });
    }

    protected override void Released()
    {
    }

    protected override object? GetClientData() => new
    {
        Connected = Vts.IsAuthed,
    };

    protected override void SettingsUpdated(PluginSettings oldSettings, PluginSettings newSettings) { }

    [PluginCommand("get-params")]
    public virtual void GetModel(PluginPayload pl)
    {
        _requestId = Guid.NewGuid().ToString();
        Vts.Send(new CurrentModelRequest(), _requestId);
    }

    public void Dispose()
    {
        VTSEvents.OnCurrentModelInformation -= CurrentModelInformation;
    }
}
using Microsoft.Extensions.Logging;
using Plugin;
using Plugin.Contracts;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Requests;
using IRequestFactory = Plugin.Contracts.Requests.IRequestFactory;

namespace Actions;

[CustomAction("dev.cazzar.vtubestudio.scalemodel")]
public class ScaleModelAction : BaseAction<ScaleModelAction.PluginSettings>
{
    public class PluginSettings
    {
        [DataMember(Name = "size")]
        public float Size { get; set; } = 0;
    }

    protected override void Pressed() => Vts.Send(new MoveModelRequest() { Size = Settings.Size, });

    protected override void Released() { }

    protected override object GetClientData() => new
    {
        Connected = Vts.IsAuthed,
    };

    protected override void SettingsUpdated(PluginSettings oldSettings, PluginSettings newSettings)
    {
    }

    public ScaleModelAction(
        IGlobalSettingsHandler gsm,
        IVTubeStudio vts,
        IConnection isd,
        ILogger<ScaleModelAction> logger,
        IRequestFactory requestFactory
    ) : base(gsm, vts, isd, logger, requestFactory)
    {
    }
}
using Microsoft.Extensions.Logging;
using Plugin;
using Plugin.Contracts;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTubeStudioAPI;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Requests;
using VTubeStudioAPI.Responses;
using IRequestFactory = Plugin.Contracts.Requests.IRequestFactory;

namespace Actions;
[CustomAction("dev.cazzar.vtubestudio.reloadmodel")]
public class ReloadCurrentModel : BaseAction<ReloadCurrentModel.PluginSettings>, IDisposable
{
    private string _requestId;

    public class PluginSettings { }

    public ReloadCurrentModel(
        IGlobalSettingsHandler gsm,
        IVTubeStudio vts,
        IConnection isd,
        ILogger<ReloadCurrentModel> logger,
        IRequestFactory requestFactory
    ) : base(gsm, vts, isd, logger, requestFactory)
    {
        VTSEvents.OnCurrentModelInformation += OnCurrentModelInfo;
    }

    private void OnCurrentModelInfo(object? sender, ApiEventArgs<CurrentModelResponse> e)
    {
        if (e.RequestId != _requestId) return;

        Vts.Send(new ModelLoadRequest(e.Response.Id));
        ShowOk();
    }

    protected override void Pressed()
    {
        Vts.Send(new CurrentModelRequest(), _requestId = Guid.NewGuid().ToString());
    }

    protected override void Released() { }

    protected override object? GetClientData() => null;
    protected override void SettingsUpdated(PluginSettings oldSettings, PluginSettings newSettings) { }

    public void Dispose()
    {
        VTSEvents.OnCurrentModelInformation -= OnCurrentModelInfo;
    }
}
using Actions.Caches;
using Actions.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
using IRequestFactory = Plugin.Contracts.Requests.IRequestFactory;

namespace Actions;

[CustomAction("dev.cazzar.vtubestudio.changemodel")]
public class ChangeModelAction : BaseAction<ChangeModelAction.PluginSettings>
{
    private readonly ILogger<ChangeModelAction> _logger;
    private readonly ModelCache _modelCache;

    static ChangeModelAction()
    {
        VTSEvents.OnModelLoad += (sender, args) => RateLimiter.ChangeModel.Trigger();
    }

    public ChangeModelAction(
        IGlobalSettingsHandler gsm, 
        IVTubeStudio vts, 
        IConnection isd, 
        ILogger<ChangeModelAction> logger, 
        IRequestFactory requestFactory, 
        ModelCache modelCache
    ) : base(gsm, vts, isd, logger, requestFactory)
    {
        _logger = logger;
        _modelCache = modelCache;
    }

    protected override void Released()
    {
    }

    protected override object? GetClientData() => new
    {
        Models = _modelCache.Models.Select(s => new VTubeReference { Id = s.Id, Name = s.Name }).ToList(),
        Connected = Vts.IsAuthed,
    };

    protected override void Pressed()
    {
        _logger.LogInformation("Key Pressed");

        if (string.IsNullOrEmpty(Settings.ModelId))
        {
            _logger.LogWarning("Key pressed but model id is null!");
            return;
        }

        if (!RateLimiter.ChangeModel.IsReady)
        {
            _logger.LogWarning("Key pressed but rate limit hit!");
            return;
        }

        Vts.Send(new ModelLoadRequest(Settings.ModelId));
    }

    protected override void SettingsUpdated(PluginSettings oldSettings, PluginSettings newSettings)
    {
        if (oldSettings.ShowName != newSettings.ShowName && !newSettings.ShowName)
            SetTitle(null);

        UpdateTitle();
    }

    private string? GetTitle() => _modelCache.Models.FirstOrDefault(m => m.Id == Settings.ModelId)?.Name;

    public override void Tick()
    {
        base.Tick();

        UpdateTitle();
    }

    public void UpdateTitle()
    {
        var title = GetTitle();

        if (!string.IsNullOrEmpty(title) && Settings.ShowName)
            SetTitle(title);
    }

    public class PluginSettings
    {
        [JsonProperty("modelId")]
        public string ModelId { get; set; } = string.Empty;

        [JsonProperty("models")]
        public List<VTubeReference> Models { get; set; } = new();

        [JsonProperty("showName")]
        public bool ShowName { get; set; } = true;
    }
}

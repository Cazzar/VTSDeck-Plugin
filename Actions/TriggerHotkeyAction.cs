﻿using Actions.Caches;
using Actions.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plugin;
using Plugin.Contracts;
using Plugin.Contracts.Actions;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Requests;

namespace Actions;

[CustomAction("dev.cazzar.vtubestudio.triggerhotkey")]
public class TriggerHotkeyAction : BaseAction<TriggerHotkeyAction.PluginSettings>
{
    private readonly ModelCache _modelCache;
    private readonly HotkeyCache _hotkeyCache;

    public class PluginSettings
    {
        [JsonProperty("modelId")]
        public string ModelId { get; set; } = string.Empty;

        [JsonProperty("hotkeyId")]
        public string HotkeyId { get; set; } = string.Empty;

        [JsonProperty("showName")]
        public bool ShowName { get; set; } = true;
    }

    protected override void Pressed()
    {
        if (string.IsNullOrEmpty(Settings.HotkeyId))
        {
            // Logger.Instance.LogMessage(TracingLevel.INFO, "Tried to press button but hotkey ID is null");
            return;
        }

        Vts.Send(new HotkeyTriggerRequest(Settings.HotkeyId));
    }

    protected override void Released()
    {
    }

    protected override object GetClientData()
    {
        var models = _modelCache.Models.Select(s => new VTubeReference() { Id = s.Id, Name = s.Name }).ToList();
        List<VTubeReference> hotkeys = new();
        if (!string.IsNullOrEmpty(Settings.ModelId) && _hotkeyCache.Hotkeys != null)
        {
            _hotkeyCache.Hotkeys.TryGetValue(Settings.ModelId, out var keys);
            hotkeys = keys?.Select(s => new VTubeReference { Id = s.Id, Name = $"{s.Name} - ({s.ButtonTitle})", }).ToList() ?? new();
        }

        return new { Models = models, Hotkeys = hotkeys, Connected = Vts.IsAuthed };
    }

    protected override void SettingsUpdated(PluginSettings oldSettings, PluginSettings newSettings)
    {
        if (newSettings.ShowName != oldSettings.ShowName && !Settings.ShowName)
            SetTitle(null);

        UpdateTitle();
    }

    public override void Refresh(PluginPayload pl)
    {
        _modelCache.Update();
        base.Refresh(pl);
    }

    public override void Tick()
    {
        base.Tick();
        UpdateTitle();
    }

    //Update the title, only if it returns a value and is enabled
    private void UpdateTitle()
    {
        var title = GetTitle();
        if (!string.IsNullOrEmpty(title) && Settings.ShowName)
            SetTitle(title);
    }

    private string GetTitle()
    {
        if (string.IsNullOrEmpty(Settings.ModelId)) return "";

        var hotkeys = _hotkeyCache.Hotkeys;
        if (!hotkeys.ContainsKey(Settings.ModelId)) return "";

        var hotkey = hotkeys[Settings.ModelId]?.FirstOrDefault(s => s.Id == Settings.HotkeyId);
        var title = hotkey?.ButtonTitle ?? "";

        if (string.IsNullOrWhiteSpace(title) && hotkey != null)
            title = hotkey.ButtonTitle;
        return title;
    }

    public TriggerHotkeyAction(
        IGlobalSettingsHandler gsm,
        IVTubeStudio vts,
        IConnection isd,
        ILogger<ScaleModelAction> logger,
        IRequestFactory requestFactory,
        HotkeyCache hotkeyCache,
        ModelCache modelCache) : base(gsm, vts, isd, logger, requestFactory)
    {
        _modelCache = modelCache;
        _hotkeyCache = hotkeyCache;
    }
}
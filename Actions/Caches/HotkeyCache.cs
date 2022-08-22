using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using VTubeStudioAPI;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Models;
using VTubeStudioAPI.Requests;
using VTubeStudioAPI.Responses;

namespace Actions.Caches;

public class HotkeyCache
{
    private readonly IVTubeStudio _vts;
    private readonly ILogger<HotkeyCache> _logger;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private static readonly List<WeakReference<HotkeyCache>> instances = new();

    public IDictionary<string, List<Hotkey>> Hotkeys => _cache;

    private readonly ConcurrentDictionary<string, List<Hotkey>> _cache = new();

    public HotkeyCache(ModelCache modelCache, IVTubeStudio vts, ILogger<HotkeyCache> logger)
    {
        instances.Add(new(this));
        _vts = vts;
        _logger = logger;
        modelCache.ModelCacheUpdated += Update;
        VTSEvents.OnModelHotkeys += OnModelHotkeys;
    }

    private async void OnModelHotkeys(object? sender, ApiEventArgs<ModelHotkeysResponse> e)
    {
        _logger.LogInformation("Hotkeys for {ModelName} ({ModelId}) updated", e.Response!.ModelName, e.Response.ModelId);
        await _semaphore.WaitAsync();

        try
        {
            if (_cache.ContainsKey(e.Response.ModelId))
                _cache.Remove(e.Response.ModelId, out var _);

            _cache.AddOrUpdate(e.Response.ModelId, e.Response.Hotkeys, (key, old) => e.Response.Hotkeys);

            Updated?.Invoke(this, new() { Hotkeys = _cache });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating hotkeys");
        }
        finally
        {
            _semaphore.Release();
        }

    }

    private void Update(object? sender, ModelCacheUpdatedEventArgs e)
    {
        if (!_vts.IsAuthed)
        {
            return;
        }

        foreach (var model in e.Models)
        {
            _logger.LogInformation("Requesting hotkeys for {Model} ({ModelId})", model.Name, model.Id);
            _vts.Send(new ModelHotkeyRequest(model));
        }
    }

    public event EventHandler<HotkeyCacheUpdatedEventArgs>? Updated;
}

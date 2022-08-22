using VTubeStudioAPI;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Models;
using VTubeStudioAPI.Requests;
using VTubeStudioAPI.Responses;
using Timer = System.Timers.Timer;

namespace Actions.Caches;

//TODO: Update this to use the Events
public class ModelCache : IDisposable
{
    private readonly IVTubeStudio _vts;
    public IEnumerable<Model> Models => _models;

    private List<Model> _models = new();

    private readonly Timer _updateTimer = new(TimeSpan.FromMinutes(10).TotalMilliseconds);

    public ModelCache(IVTubeStudio vts)
    {
        _vts = vts;
        VTSEvents.OnAuthenticationResponse += (sender, args) =>
        {
            if (args.Response!.Authenticated)
                Update();
        };

        VTSEvents.OnAvailableModels += OnAvailableModels;

        _updateTimer.Elapsed += (sender, args) => Update();
        _updateTimer.Start();

    }

    private void OnAvailableModels(object? sender, ApiEventArgs<AvailableModelsResponse> e)
    {
        _models = e.Response!.Models.Distinct(new Model.IdEqualityComparer()).ToList();
        ModelCacheUpdated?.Invoke(this, new() { Models = _models });
    }

    internal void Update()
    {
        if (!_vts.IsAuthed)
            return;

        _vts.Send(new AvailableModelsRequest());
    }

    public event EventHandler<ModelCacheUpdatedEventArgs>? ModelCacheUpdated;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _updateTimer.Dispose();
    }
}

public class ModelCacheUpdatedEventArgs : EventArgs
{
    public List<Model> Models { get; set; } = new();
}
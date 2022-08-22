using VTubeStudioAPI.Models;

namespace Actions.Caches;

public class HotkeyCacheUpdatedEventArgs : EventArgs
{
    public IDictionary<string, List<Hotkey>> Hotkeys { get; init; }
}
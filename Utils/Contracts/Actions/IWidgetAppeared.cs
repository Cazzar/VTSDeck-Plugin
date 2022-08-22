using Newtonsoft.Json.Linq;

namespace Plugin.Contracts.Actions;

public interface IWidgetAppeared : IActionReference
{
    JObject? GetSettings();
    T? GetSettings<T>();
}

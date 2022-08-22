using Newtonsoft.Json.Linq;

namespace Plugin.Contracts;

public interface IWidgetSettings : IActionReference
{
    JObject GetSettings();
    T? GetSettings<T>() => GetSettings().ToObject<T>();
}

using Newtonsoft.Json.Linq;
using Plugin.Contracts;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

class DidReceiveWidgetSettings : ContextMessage, IWidgetSettings
{
    public JObject Payload { get; set; } = new();

    public JObject GetSettings() => Payload;
    public T? GetSettings<T>() => GetSettings().ToObject<T>();
}



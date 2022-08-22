using Newtonsoft.Json.Linq;
using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class WidgetWillAppear : ContextMessage, IWidgetAppeared
{
    public WidgetAppearPayload Payload { get; set; }

    public JObject? GetSettings() => Payload.Settings;
    public T? GetSettings<T>() => GetSettings().ToObject<T>();
}


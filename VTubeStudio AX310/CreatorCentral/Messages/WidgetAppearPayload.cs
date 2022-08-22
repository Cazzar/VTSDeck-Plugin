using Newtonsoft.Json.Linq;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class WidgetAppearPayload : StatePayload
{
    public JObject Settings { get; set; }
}


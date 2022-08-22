using Newtonsoft.Json.Linq;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class DidReceiveWidgetIcon : ContextMessage
{
    public JObject Payload { get; set; }
}


using Newtonsoft.Json.Linq;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class DidReceiveWidgetTitle : ContextMessage
{
    public JObject Payload { get; set; }
}


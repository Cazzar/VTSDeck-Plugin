using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class WidgetWillDisappear : ContextMessage, IWidgetDisppeared
{
    public StatePayload Payload { get; set; }
}


using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class ActionTriggered : ContextMessage
{
    public StatePayload Payload { get; set; }
}


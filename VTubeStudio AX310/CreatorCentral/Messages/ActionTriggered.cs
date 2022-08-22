using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class ActionTriggered : ContextMessage, IKeyDown
{
    public StatePayload Payload { get; set; }
}


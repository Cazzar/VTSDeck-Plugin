using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class ActionDown : ContextMessage, IKeyDown
{
    public StatePayload Payload { get; set; }
}


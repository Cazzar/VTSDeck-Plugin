using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class KeyUp : ContextMessage, IKeyUp
{
    public StatePayload Payload { get; set; }
}


using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

class KeyDown : ContextMessage, IKeyDown
{
    public StatePayload Payload { get; set; }
}



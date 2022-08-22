using Plugin.Contracts;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class ContextMessage : BaseMessage, IActionReference
{
    public string Context { get; set; }
    public string Widget { get; set; }

    public string ContextId => Context;

    public string Action => Widget;
}



using Plugin.Contracts;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;
internal class BaseMessage : IMessage
{
    public string Event { get; set; }
}

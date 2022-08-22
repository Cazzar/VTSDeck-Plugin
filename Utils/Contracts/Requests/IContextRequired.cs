namespace Plugin.Contracts.Requests;

public interface IContextRequired : IMessage
{
    string ContextId { get; set; }
}
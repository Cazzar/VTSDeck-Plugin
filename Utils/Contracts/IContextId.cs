namespace Plugin.Contracts;

public interface IActionReference
{
    string ContextId { get; }
    string Action { get; }
}
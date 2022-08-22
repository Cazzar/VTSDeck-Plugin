namespace Plugin.Contracts;

internal interface IContextAware
{
    string? ContextId { get; set; }
}
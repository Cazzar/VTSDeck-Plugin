namespace Plugin.Contracts.Requests;

public interface IRequestSettings : IContextRequired
{
}

public interface ISetTitle : IContextRequired
{
    string? Title { get; set; }
}
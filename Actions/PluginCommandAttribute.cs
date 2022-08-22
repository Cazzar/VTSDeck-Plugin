namespace Actions;

[AttributeUsage(AttributeTargets.Method)]
internal class PluginCommandAttribute : Attribute
{
    public string Command { get; init; }
    public PluginCommandAttribute(string command)
    {
        this.Command = command;
    }
}
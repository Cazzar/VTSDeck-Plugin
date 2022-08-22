namespace Plugin;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CustomActionAttribute : Attribute
{
    public string ActionId { get; init; }

    public CustomActionAttribute(string actionId)
    {
        ActionId = actionId;
    }
}
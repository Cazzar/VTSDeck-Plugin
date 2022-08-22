namespace Plugin.Contracts;

public interface IGlobalSettings
{
    T? GetSettings<T>();
}

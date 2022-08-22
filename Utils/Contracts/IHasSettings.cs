using Newtonsoft.Json.Linq;

namespace Plugin.Contracts;

public interface IHasSettings
{
    void GotSettings(JObject? settings);
}
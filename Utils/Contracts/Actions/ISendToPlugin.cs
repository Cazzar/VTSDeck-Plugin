using Newtonsoft.Json.Linq;

namespace Plugin.Contracts.Actions;

public interface ISendToPlugin : IActionReference
{
    T? GetPayload<T>();
    JObject GetPayload();
}
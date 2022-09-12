using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Actions;

public class PluginPayload
{
    [JsonProperty("command")]
    public string Command { get; set; }

    [JsonProperty("payload")]
    public JObject Payload { get; set; }

    public string Context { get; set; }

    public T GetPayload<T>() => Payload.ToObject<T>();
}
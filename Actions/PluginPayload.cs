using Newtonsoft.Json;

namespace Actions;

public class PluginPayload
{
    [JsonProperty("command")]
    public string Command { get; set; }

    [JsonProperty("payload")]
    public dynamic Payload { get; set; }

    public string Context { get; set; }
}
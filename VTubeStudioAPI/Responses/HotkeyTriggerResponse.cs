using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class HotkeyTriggerResponse
{
    [JsonProperty("hotkeyID")]
    public string Id { get; set; }
}

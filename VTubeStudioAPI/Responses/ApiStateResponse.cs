using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class ApiStateResponse
{
    [JsonProperty("active")]
    public bool Active { get; set; }
    [JsonProperty("vTubeStudioVersion")]
    public string Version { get; set; }
    [JsonProperty("currentSessionAuthenticated")]
    public bool Authenticated { get; set; }
}

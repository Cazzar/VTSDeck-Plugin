using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class AuthenticationResponse
{
    [JsonProperty("authenticated")] public bool Authenticated { get; set; }
    [JsonProperty("reason")] public string Reason { get; set; }
}

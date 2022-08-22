using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class AuthenticateResponse
{
    [JsonProperty("authenticationToken")]
    public string AuthToken { get; set; }
}
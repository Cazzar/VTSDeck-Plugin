using VTubeStudioAPI.Responses;
using Newtonsoft.Json;

namespace VTubeStudioAPI.Requests;

[AuthLess]
public class AuthWithTokenRequest : ApiRequest
{
    [JsonProperty("pluginName")] 
    public string Name { get; } = "StreamDeck Integration";
    [JsonProperty("pluginDeveloper")] 
    public string Developer { get; } = "Cazzar";

    [JsonProperty("authenticationToken")]
    public string Token { get; set; }

    public AuthWithTokenRequest(string token)
    {
        this.Token = token;
    }
    
    public override RequestType MessageType { get; } = RequestType.AuthenticationRequest;
}

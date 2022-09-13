using VTubeStudioAPI.Responses;
using Newtonsoft.Json;
using VTubeStudioAPI.Contracts;

namespace VTubeStudioAPI.Requests;

[AuthLess]
public class AuthWithTokenRequest : IApiRequest
{
    [JsonProperty("pluginName")] 
    public string Name { get; }
    [JsonProperty("pluginDeveloper")] 
    public string Developer { get; }

    [JsonProperty("authenticationToken")]
    public string Token { get; set; }

    public AuthWithTokenRequest(IPluginInformation info)
    {
        Name = info.Name;
        Developer = info.Developer;
    }

    public RequestType MessageType { get; } = RequestType.AuthenticationRequest;
}

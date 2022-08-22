using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

public abstract class ApiRequest
{
    [JsonIgnore]
    public abstract RequestType MessageType { get; }
}

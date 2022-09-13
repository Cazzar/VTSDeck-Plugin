using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

public interface IApiRequest
{
    [JsonIgnore]
    public abstract RequestType MessageType { get; }
}

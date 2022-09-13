using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI.Requests;
public class RequestWrapper
{
    [JsonProperty("apiName")] public string ApiName { get; } = "VTubeStudioPublicAPI";
    [JsonProperty("apiVersion")] public string ApiVersion { get; } = "1.0";
    [JsonProperty("messageType")] public string MessageType { get; init; }
    [JsonProperty("data")] public IApiRequest Data { get; init; }
    [JsonProperty("requestID", NullValueHandling = NullValueHandling.Ignore)] public string? RequestId { get; init; }

    public RequestWrapper(IApiRequest request)
    {
        MessageType = request.MessageType.ToString();
        Data = request;
    }
}
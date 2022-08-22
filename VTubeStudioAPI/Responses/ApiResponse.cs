using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI.Responses;
public class ApiResponse<T> : ApiResponse
{
    [JsonProperty("data")]
    public T? Data { get; init; } = default;
}

public class ApiResponse
{
    [JsonProperty("apiName")]
    public string ApiName { get; set; } = "";

    [JsonProperty("apiVersion")]
    public string ApiVersion { get; set; } = "";

    [JsonProperty("timestamp")]
    public ulong Timestamp { get; set; }

    [JsonProperty("requestId")]
    public string RequestId { get; set; } = "";


    [JsonProperty("messageType")]
    public string MessageType { get; init; } = "";
}
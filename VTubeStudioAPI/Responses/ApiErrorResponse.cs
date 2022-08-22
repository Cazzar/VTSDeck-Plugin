using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class ApiErrorResponse
{
    [JsonProperty("errorID")]
    public ApiError Id { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }
}

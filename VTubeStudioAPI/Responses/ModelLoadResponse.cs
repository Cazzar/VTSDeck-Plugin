using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class ModelLoadResponse
{
    [JsonProperty("modelID")]
    public string Id { get; set; }
}

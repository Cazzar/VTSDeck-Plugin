using System.Collections.Generic;
using VTubeStudioAPI.Models;
using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class AvailableModelsResponse
{
    [JsonProperty("numberOfModels")]
    public int Count { get; set; }

    [JsonProperty("availableModels")]
    public List<Model> Models { get; set; }
}

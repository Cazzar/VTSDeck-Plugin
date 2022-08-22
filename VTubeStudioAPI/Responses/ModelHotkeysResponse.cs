using System.Collections.Generic;
using VTubeStudioAPI.Models;
using Newtonsoft.Json;

namespace VTubeStudioAPI.Responses;

public class ModelHotkeysResponse
{
    [JsonProperty("modelLoaded")]
    public bool ModelLoaded { get; set; }
    [JsonProperty("modelName")]
    public string ModelName { get; set; }
    [JsonProperty("ModelId")]
    public string ModelId { get; set; }
    [JsonProperty("availableHotkeys")]
    public List<Hotkey> Hotkeys { get; set; }
}

using System.Diagnostics.CodeAnalysis;
using VTubeStudioAPI.Models;
using VTubeStudioAPI.Responses;
using Newtonsoft.Json;

namespace VTubeStudioAPI.Requests;

public class ModelHotkeyRequest : IApiRequest
{
    [JsonProperty("modelID")]
    public string ModelId { get; set; }
    
    public ModelHotkeyRequest([MaybeNull] string modelId)
    {
        ModelId = modelId;
    }

    public ModelHotkeyRequest(Model model) : this(model.Id)
    {
    }

    public ModelHotkeyRequest()
    {
        
    }

    public RequestType MessageType { get; } = RequestType.HotkeysInCurrentModelRequest;
}

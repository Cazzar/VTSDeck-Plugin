using VTubeStudioAPI.Models;
using VTubeStudioAPI.Responses;
using Newtonsoft.Json;

namespace VTubeStudioAPI.Requests;

public class HotkeyTriggerRequest : IApiRequest
{
    [JsonProperty("hotkeyID")]
    public string Id { get; set; }

    public HotkeyTriggerRequest(string hotkeyId)
    {
        this.Id = hotkeyId;
    }

    public HotkeyTriggerRequest(Hotkey hotkey) : this(hotkey.Id)
    {
        
    }

    public RequestType MessageType { get; } = RequestType.HotkeyTriggerRequest;
}

using VTubeStudioAPI.Responses;
using Newtonsoft.Json;

namespace VTubeStudioAPI.Requests;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class MoveModelRequest : IApiRequest
{
    [JsonProperty("timeInSeconds", NullValueHandling = NullValueHandling.Include)] public double? TimeInSeconds { get; set; }
    [JsonProperty("valuesAreRelativeToModel")] public bool? RelativeMove { get; set; }
    [JsonProperty("positionX")] public double? PositionX { get; set; }
    [JsonProperty("positionY")] public double? PositionY { get; set; }
    [JsonProperty("rotation")] public double? Rotation { get; set; }
    [JsonProperty("size")] public double? Size { get; set; }
    
    public RequestType MessageType { get; } = RequestType.MoveModelRequest;
}

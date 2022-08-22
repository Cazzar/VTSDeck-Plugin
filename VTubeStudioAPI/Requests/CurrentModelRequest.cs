using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

public class CurrentModelRequest : ApiRequest
{
    public override RequestType MessageType { get; } = RequestType.CurrentModelRequest;
}

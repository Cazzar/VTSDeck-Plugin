using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

public class CurrentModelRequest : IApiRequest
{
    public RequestType MessageType { get; } = RequestType.CurrentModelRequest;
}

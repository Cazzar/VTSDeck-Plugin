using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

public class AvailableModelsRequest : ApiRequest
{
    public override RequestType MessageType { get; } = RequestType.AvailableModelsRequest;
}

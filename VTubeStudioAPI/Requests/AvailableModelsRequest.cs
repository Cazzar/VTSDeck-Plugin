using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

public class AvailableModelsRequest : IApiRequest
{
    public RequestType MessageType { get; } = RequestType.AvailableModelsRequest;
}

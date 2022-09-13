using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

[AuthLess]
public class ApiStateRequest : IApiRequest
{
    public RequestType MessageType { get; } = RequestType.APIStateRequest;
}

using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Requests;

[AuthLess]
public class ApiStateRequest : ApiRequest
{
    public override RequestType MessageType { get; } = RequestType.APIStateRequest;
}

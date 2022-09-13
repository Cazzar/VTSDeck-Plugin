using VTubeStudioAPI.Requests;

namespace VTubeStudioAPI.Contracts;

public interface IRequestFactory
{
    T? CreateRequest<T>(Action<T>? configure = null) where T : IApiRequest;
}
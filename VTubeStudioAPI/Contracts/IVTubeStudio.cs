using VTubeStudioAPI.Requests;
using VTubeStudioAPI.Responses;

namespace VTubeStudioAPI.Contracts;
public interface IVTubeStudio
{
    bool Authed { get; }
    bool IsAuthed { get; }

    public void Send(ApiRequest request, string? requestId = null);
    public void SetConnectionParams(string host, ushort port);
    public void EnsureConnected();
    public void Reconnect();
    void Run();
    void Stop();
}

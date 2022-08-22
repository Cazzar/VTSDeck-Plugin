using Plugin.Contracts;

namespace AverMediaVTubeStudio.CreatorCentral;

interface ICreatorCentralConnection : IConnection
{
    Task Run();
    Task Cancel();
}

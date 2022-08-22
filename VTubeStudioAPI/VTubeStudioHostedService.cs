using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VTubeStudioAPI.Contracts;

namespace VTubeStudioAPI;

public class VTubeStudioHostedService : IHostedService
{
    private readonly IHostApplicationLifetime lifetime;
    private readonly ILogger<VTubeStudioHostedService> logger;
    private readonly IVTubeStudio connection;

    public VTubeStudioHostedService(
        ILogger<VTubeStudioHostedService> logger,
        IVTubeStudio connection,
        IHostApplicationLifetime lifetime
    )
    {
        logger.LogTrace("Hosted service created");
        this.lifetime = lifetime;
        this.logger = logger;
        this.connection = connection;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        lifetime.ApplicationStarted.Register(() => Task.Run(() => OnStarted(cancellationToken), cancellationToken));
        lifetime.ApplicationStopping.Register(OnStopping);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void OnStopping()
    {
        logger.LogTrace("Stopping the VTubeStudio HostedService");
        connection.Stop();
    }

    private void OnStarted(CancellationToken cancellationToken)
    {
        logger.LogTrace("Starting up the VTubeStudio HostedService");

        connection.Run();
    }
}
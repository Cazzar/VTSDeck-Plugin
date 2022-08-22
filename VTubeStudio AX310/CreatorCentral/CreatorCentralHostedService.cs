using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Plugin.Contracts.Actions;
using System.Net.WebSockets;

namespace AverMediaVTubeStudio.CreatorCentral;
internal class CreatorCentralHostedService : IHostedService
{
    private readonly IHostApplicationLifetime lifetime;
    private readonly IActionRepository actionRepository;
    private readonly ILogger<CreatorCentralHostedService> logger;
    private readonly ICreatorCentralConnection connection;

    public CreatorCentralHostedService(
        ILogger<CreatorCentralHostedService> logger,
        ICreatorCentralConnection connection,
        IHostApplicationLifetime lifetime,
        IActionRepository actionRepository
    )
    {
        logger.LogTrace("Hosted service created");
        this.lifetime = lifetime;
        this.actionRepository = actionRepository;
        this.logger = logger;
        this.connection = connection;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        actionRepository.FindActions();

        lifetime.ApplicationStarted.Register(() => Task.Run(() => OnStarted(cancellationToken), cancellationToken));
        lifetime.ApplicationStopping.Register(OnStopping);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private async void OnStopping()
    {
        logger.LogTrace(nameof(OnStopping));
        await connection.Cancel();
    }

    private async void OnStarted(CancellationToken cancellationToken)
    {
        logger.LogTrace(nameof(OnStarted));

        await connection.Run();   
    }
}

using Microsoft.Extensions.Hosting;
using Plugin.Contracts.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin;
internal class ActionTimer : IHostedService
{
    private readonly IActionRepository _repository;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly CancellationTokenSource _cancellationToken = new();

    public ActionTimer(IActionRepository repository, IHostApplicationLifetime lifetime)
    {
        _repository = repository;
        _lifetime = lifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _lifetime.ApplicationStarted.Register(() => Task.Run(OnStarted, cancellationToken));
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        while (!_cancellationToken.IsCancellationRequested)
        {
            _repository.Tick();

            Thread.Sleep(1000);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationToken.Cancel();
        return Task.CompletedTask;
    }
}
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AverMediaVTubeStudio.CreatorCentral;
using System.Net.WebSockets;
using System.Diagnostics.Contracts;
using AverMediaVTubeStudio.CreatorCentral.Messages;
using System.Text.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using AverMediaVTubeStudio.Requests;
using Plugin.Contracts.Requests;
using VTubeStudioAPI;
using Plugin.Contracts;
using VTubeStudioAPI.Contracts;
using Plugin;
using Plugin.Contracts.Actions;
using System.Runtime.CompilerServices;
using VTubeStudioAPI.Responses;
using Actions;
using Actions.Caches;
using NLog.Extensions.Logging;
using IRequestFactory = Plugin.Contracts.Requests.IRequestFactory;
using RequestFactory = Plugin.RequestFactory;



namespace AverMediaVTubeStudio;

public class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = CreateHostBuilder(args);
        await hostBuilder.Build().RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();


        return Host.CreateDefaultBuilder(args)
            .ConfigureCommandLine(args)
            .ConfigureServices(
                (ctx, services) => {
                    services.AddSingleton<AuthManager>();
                    services.AddSingleton<IGlobalSettingsHandler>(s => s.GetRequiredService<AuthManager>());
                    
                    services.AddVTubeStudio(
                        services => services.GetRequiredService<AuthManager>(),
                        services => services.GetRequiredService<AuthManager>()
                    );

                    services.AddSingleton<CreatorCentralConnection>();
                    services.AddSingleton<ICreatorCentralConnection>(x => x.GetRequiredService<CreatorCentralConnection>());
                    services.AddSingleton<IConnection>(x => x.GetRequiredService<CreatorCentralConnection>());
                    services.AddSingleton(x => new Lazy<IConnection>(() => x.GetRequiredService<CreatorCentralConnection>()));

                    services.AddTransient<IRequestFactory, RequestFactory>();
                    services.AddTransient<IRequestSettings, RequestSettings>();
                    services.AddTransient<ISaveSettings, SaveSettings>();
                    services.AddTransient<ISaveGlobalSettings, SetPackageSettings>();
                    services.AddTransient<ISendToPropertyView, SendToPropertyView>();
                    services.AddTransient<IRequestGlobalSettings, RequestPackageSettings>();

                    services.AddHostedService<CreatorCentralHostedService>();
                    services.Configure<RegistrationOptions>(ctx.Configuration.GetSection(Extensions.Prefix));
                    services.AddSingleton<IPluginInformation, VTubeStudioPluginInformation>();

                    services.AddActionRepository();
                    services.AddLazySupport();
                    services.AddActions();

                    services.AddLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                        logging.AddNLog();
                    });
                }
            );
    }
}

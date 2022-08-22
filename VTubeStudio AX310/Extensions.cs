using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AverMediaVTubeStudio;

internal static class Extensions
{
    internal const string Prefix = "CreatorCentral";

    public static IHostBuilder ConfigureCommandLine(this IHostBuilder hostBuilder, string[] args)
    {
        hostBuilder
            .ConfigureAppConfiguration(config =>
            {
                config.AddCommandLine(args, new Dictionary<string, string>
                {
                    { "-port", $"{Prefix}:Port" },
                    { "-packageUUID", $"{Prefix}:Uuid" },
                    { "-registerEvent", $"{Prefix}:RegisterEvent" },
                    { "-info", $"{Prefix}:Info" }
                });
            });

        return hostBuilder;
    }
}

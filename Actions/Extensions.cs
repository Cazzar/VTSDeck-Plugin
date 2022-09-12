using Actions.Caches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        services.AddSingleton<ModelCache>();
        services.AddSingleton<HotkeyCache>();


        return services;
    }
}

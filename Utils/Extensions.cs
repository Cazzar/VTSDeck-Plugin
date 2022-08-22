using Microsoft.Extensions.DependencyInjection;
using Plugin;
using Plugin.Contracts.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;
public static class Extensions
{
    public static IServiceCollection AddActionRepository(this IServiceCollection services)
    {
        services.AddSingleton<IActionRepository, ActionRepository>();
        return services;
    }

    public static IServiceCollection AddLazySupport(this IServiceCollection services)
    {
        return services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
    }
}

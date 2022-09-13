using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Requests;

namespace VTubeStudioAPI;
public static class VTubeStudioExtensions
{
    public static IServiceCollection AddVTubeStudio<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] U
    >(this IServiceCollection services)
        where T : IAuthManager
        where U : IVTubeStudioSettings
    {
        services.AddSingleton<IVTubeStudio, VTubeStudioImpl>();
        services.AddSingleton(typeof(IAuthManager), typeof(T));
        services.AddSingleton(typeof(IVTubeStudioSettings), typeof(U));
        services.AddScoped<IRequestFactory, RequestFactory>();

        return services;
    }

    public static IServiceCollection AddVTubeStudio(
        this IServiceCollection services,
        Func<IServiceProvider, IAuthManager> getAuthManager,
        Func<IServiceProvider, IVTubeStudioSettings> getSettings
    )
    {
        services.AddSingleton<IVTubeStudio, VTubeStudioImpl>();
        services.AddSingleton(getAuthManager);
        services.AddSingleton(getSettings);
        services.AddScoped<IRequestFactory, RequestFactory>();
        services.AddTransient<AuthenticateRequest>();
        services.AddTransient<AuthWithTokenRequest>();

        return services;
    }
}

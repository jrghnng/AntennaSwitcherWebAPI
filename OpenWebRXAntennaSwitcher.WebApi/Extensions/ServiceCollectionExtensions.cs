using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OpenWebRXAntennaSwitcher.WebApi.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add an <see cref="IHostedService"/> registration for <typeparamref name="THostedService"/>
    /// and registers it as singleton service for <typeparamref name="TService"/>.
    /// </summary>
    /// <typeparam name="TService">The service type.</typeparam>
    /// <typeparam name="THostedService">The <see cref="IHostedService"/> to register.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to register with.</param>
    /// <returns>The original <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddHostedService<TService, THostedService>(this IServiceCollection services)
        where TService : class
        where THostedService : class, TService, IHostedService
    {
        services.AddSingleton<THostedService, THostedService>();
        services.AddSingleton<TService, THostedService>(provider => provider.GetRequiredService<THostedService>());
        services.AddHostedService(provider => provider.GetRequiredService<THostedService>());
        return services;
    }
}

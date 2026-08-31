using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Rebrickable.Client.Registrars;
using Soenneker.Rebrickable.ClientUtil.Abstract;

namespace Soenneker.Rebrickable.ClientUtil.Registrars;

/// <summary>
/// Registers the lazily initialized Rebrickable API client.
/// </summary>
public static class RebrickableClientUtilRegistrar
{
    /// <summary>
    /// Adds the Rebrickable API client utility as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddRebrickableClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddRebrickableHttpClientAsSingleton().TryAddSingleton<IRebrickableClientUtil, RebrickableClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds the Rebrickable API client utility as a scoped service backed by the singleton HTTP client provider. <para/>
    /// </summary>
    public static IServiceCollection AddRebrickableClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddRebrickableHttpClientAsSingleton().TryAddScoped<IRebrickableClientUtil, RebrickableClientUtil>();

        return services;
    }
}

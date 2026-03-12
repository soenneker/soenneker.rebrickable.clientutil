using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Rebrickable.Client.Registrars;
using Soenneker.Rebrickable.ClientUtil.Abstract;

namespace Soenneker.Rebrickable.ClientUtil.Registrars;

/// <summary>
/// A .NET thread-safe singleton HttpClient for GitHub
/// </summary>
public static class RebrickableClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="RebrickableClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddRebrickableClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddRebrickableHttpClientAsSingleton().TryAddSingleton<IRebrickableClientUtil, RebrickableClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="RebrickableClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddRebrickableClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddRebrickableHttpClientAsSingleton().TryAddScoped<IRebrickableClientUtil, RebrickableClientUtil>();

        return services;
    }
}
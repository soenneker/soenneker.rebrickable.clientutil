using Soenneker.Rebrickable.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Rebrickable.ClientUtil.Abstract;

/// <summary>
/// A .NET thread-safe singleton HttpClient for 
/// </summary>
public interface IRebrickableClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<RebrickableOpenApiClient> Get(CancellationToken cancellationToken = default);
}

using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Rebrickable.Client.Abstract;
using Soenneker.Rebrickable.ClientUtil.Abstract;
using Soenneker.Rebrickable.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Rebrickable.ClientUtil;

public sealed class RebrickableClientUtil : IRebrickableClientUtil
{
    private readonly AsyncSingleton<RebrickableOpenApiClient> _client;
    private readonly IRebrickableHttpClient _httpClientUtil;

    public RebrickableClientUtil(IRebrickableHttpClient httpClientUtil, IConfiguration _)
    {
        _httpClientUtil = httpClientUtil;
        _client = new AsyncSingleton<RebrickableOpenApiClient>(CreateClient);
    }

    private async ValueTask<RebrickableOpenApiClient> CreateClient(CancellationToken token)
    {
        HttpClient httpClient = await _httpClientUtil.Get(token).NoSync();

        var requestAdapter = new HttpClientRequestAdapter(new Microsoft.Kiota.Abstractions.Authentication.AnonymousAuthenticationProvider(), httpClient: httpClient)
        {
            BaseUrl = httpClient.BaseAddress!.ToString().TrimEnd('/')
        };

        return new RebrickableOpenApiClient(requestAdapter);
    }

    public ValueTask<RebrickableOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}

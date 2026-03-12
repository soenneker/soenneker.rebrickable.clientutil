using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Rebrickable.Client.Abstract;
using Soenneker.Rebrickable.ClientUtil.Abstract;
using Soenneker.Rebrickable.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Rebrickable.ClientUtil;

///<inheritdoc cref="IRebrickableClientUtil"/>
public sealed class RebrickableClientUtil : IRebrickableClientUtil
{
    private readonly AsyncSingleton<RebrickableOpenApiClient> _client;
    private readonly IRebrickableHttpClient _httpClientUtil;
    private readonly IConfiguration _configuration;

    public RebrickableClientUtil(IRebrickableHttpClient httpClientUtil, IConfiguration configuration)
    {
        _httpClientUtil = httpClientUtil;
        _configuration = configuration;
        _client = new AsyncSingleton<RebrickableOpenApiClient>(CreateClient);
    }

    private async ValueTask<RebrickableOpenApiClient> CreateClient(CancellationToken token)
    {
        HttpClient httpClient = await _httpClientUtil.Get(token).NoSync();

        var apiKey = _configuration.GetValueStrict<string>("Rebrickable:ApiKey");

        var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider("Authorization", $"key {apiKey}"), httpClient: httpClient);

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

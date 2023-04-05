using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace RestClientDemo.RestClients;

public class RestClient : IRestClient
{
    private readonly HttpClient _httpClient;

    public RestClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RestModel>> GetModelsAsync(int startId, int pageSize, CancellationToken cancellationToken)
    {
        Dictionary<string, string> parameters = new()
        {
            { "StartId", startId.ToString() },
            { "PageSize", pageSize.ToString() }
        };

        var requestUri = QueryHelpers.AddQueryString(string.Empty, parameters);

        var models = await _httpClient.GetFromJsonAsync<IEnumerable<RestModel>>(requestUri, cancellationToken);

        return models ?? Enumerable.Empty<RestModel>();
    }
}

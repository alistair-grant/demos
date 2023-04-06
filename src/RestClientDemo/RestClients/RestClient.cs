using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text.Json;

namespace RestClientDemo.RestClients;

public class RestClient : IRestClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public RestClient(HttpClient httpClient, JsonSerializerOptions serializerOptions)
    {
        _httpClient = httpClient;
        _serializerOptions = serializerOptions;
    }

    public async Task<IEnumerable<RestModel>> GetModelsAsync(int startId, int pageSize, CancellationToken cancellationToken)
    {
        static IEnumerable<RestModel> EmptySequence() => Enumerable.Empty<RestModel>();

        Dictionary<string, string> parameters = new()
        {
            { "StartId", startId.ToString() },
            { "PageSize", pageSize.ToString() }
        };

        var requestUri = QueryHelpers.AddQueryString(string.Empty, parameters);
        var response = await _httpClient.GetAsync(requestUri, cancellationToken);
        var statusCode = response.StatusCode;

        if (statusCode == HttpStatusCode.NoContent || statusCode == HttpStatusCode.NotFound)
        {
            return EmptySequence();
        }

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
            return EmptySequence();
        }

        var models = JsonSerializer.Deserialize<IEnumerable<RestModel>>(content, _serializerOptions);

        return models ?? EmptySequence();
    }
}

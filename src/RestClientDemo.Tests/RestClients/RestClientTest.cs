// Adapted from https://methodpoet.com/unit-test-httpclient/

using Microsoft.AspNetCore.WebUtilities;
using Moq;
using Moq.Protected;
using RestClientDemo.Mocks;
using System.Net;
using System.Text.Json;

namespace RestClientDemo.RestClients;

public class RestClientTest
{
    private const int DefaultPageSize = 100;

    private readonly JsonSerializerOptions DefaultSerializerOptions = new();

    private readonly Uri DefaultBaseAddress = new("http://tempuri.org/models");

    [Theory]
    [InlineData(HttpStatusCode.NoContent, null)]
    [InlineData(HttpStatusCode.NotFound, null)]
    [InlineData(HttpStatusCode.OK, "")]
    [InlineData(HttpStatusCode.OK, "[]")]
    [InlineData(HttpStatusCode.OK, null)]
    public async Task GetModelsAsync_NoModelsResponse_ReturnsEmptySequence(HttpStatusCode statusCode, string? content)
    {
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        mockHttpMessageHandler.Protected()
            .SetupSendAsync()
            .ReturnsHttpResponseMessageAsync(statusCode, content);

        HttpClient httpClient = new(mockHttpMessageHandler.Object)
        {
            BaseAddress = DefaultBaseAddress
        };

        RestClient restClient = new(httpClient, DefaultSerializerOptions);

        var actual = await restClient.GetModelsAsync(0, DefaultPageSize, CancellationToken.None);

        Assert.Empty(actual);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public async Task GetModelsAsync_PageSizeUriQueryParameter_HasExpectedValue(int expected)
    {
        int? actual = null;

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        mockHttpMessageHandler.Protected()
            .SetupSendAsync()
            .Callback((HttpRequestMessage request, CancellationToken _) =>
                actual = request?.RequestUri?.ParseQueryParameterAsInt("PageSize"))
            .ReturnsNoContentAsync();

        HttpClient httpClient = new(mockHttpMessageHandler.Object)
        {
            BaseAddress = DefaultBaseAddress
        };

        RestClient restClient = new(httpClient, DefaultSerializerOptions);

        await restClient.GetModelsAsync(0, expected, CancellationToken.None);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public async Task GetModelsAsync_StartIdUriQueryParameter_HasExpectedValue(int expected)
    {
        int? actual = null;

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        mockHttpMessageHandler.Protected()
            .SetupSendAsync()
            .Callback((HttpRequestMessage request, CancellationToken _) =>
                actual = request?.RequestUri?.ParseQueryParameterAsInt("StartId"))
            .ReturnsNoContentAsync();

        HttpClient httpClient = new(mockHttpMessageHandler.Object)
        {
            BaseAddress = DefaultBaseAddress
        };

        RestClient restClient = new(httpClient, DefaultSerializerOptions);

        await restClient.GetModelsAsync(expected, DefaultPageSize, CancellationToken.None);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetModelsAsync_UriPath_HasExpectedValue()
    {
        string expectedValue = DefaultBaseAddress.AbsolutePath;

        string? actualValue = null;

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        mockHttpMessageHandler.Protected()
            .SetupSendAsync()
            .Callback((HttpRequestMessage request, CancellationToken _) =>
                actualValue = request?.RequestUri?.AbsolutePath)
            .ReturnsNoContentAsync();

        HttpClient httpClient = new(mockHttpMessageHandler.Object)
        {
            BaseAddress = DefaultBaseAddress
        };

        RestClient restClient = new(httpClient, DefaultSerializerOptions);

        await restClient.GetModelsAsync(0, DefaultPageSize, CancellationToken.None);

        Assert.Equal(expectedValue, actualValue);
    }
}

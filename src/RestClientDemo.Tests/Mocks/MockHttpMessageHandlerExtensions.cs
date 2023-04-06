using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using System.Net;

namespace RestClientDemo.Mocks;

public static class MockHttpMessageHandlerExtensions
{
    public static IReturnsResult<HttpMessageHandler> ReturnsNoContentAsync(
        this IReturnsThrows<HttpMessageHandler, Task<HttpResponseMessage>> returnsThrows)
    {
        return returnsThrows.ReturnsHttpResponseMessageAsync(HttpStatusCode.NoContent, null);
    }

    public static IReturnsResult<HttpMessageHandler> ReturnsHttpResponseMessageAsync(
        this IReturnsThrows<HttpMessageHandler, Task<HttpResponseMessage>> returnsThrows,
        HttpStatusCode statusCode,
        string? content)
    {
        return returnsThrows.ReturnsAsync(new HttpResponseMessage
        {
            StatusCode = statusCode,
            Content = content != null ? new StringContent(content) : null
        });
    }

    public static ISetup<HttpMessageHandler, Task<HttpResponseMessage>> SetupSendAsync(
        this IProtectedMock<HttpMessageHandler> protectedMock)
    {
        return protectedMock
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
    }
}

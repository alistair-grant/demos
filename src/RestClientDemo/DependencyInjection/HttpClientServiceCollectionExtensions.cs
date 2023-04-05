using Microsoft.Extensions.DependencyInjection;
using RestClientDemo.RestClients;

namespace RestClientDemo.DependencyInjection;

public static class HttpClientServiceCollectionExtensions
{
    public static IServiceCollection AddRestClient(this IServiceCollection services)
    {
        services.AddHttpClient<IRestClient, RestClient>(client =>
        {
            client.BaseAddress = new Uri("http://tempuri.org/models");
        });

        return services;
    }
}

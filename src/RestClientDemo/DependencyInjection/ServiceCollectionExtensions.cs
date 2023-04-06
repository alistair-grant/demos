using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace RestClientDemo.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddRestClient();

        services.AddSingleton(new JsonSerializerOptions());
        services.AddSingleton<Program>();

        return services;
    }
}

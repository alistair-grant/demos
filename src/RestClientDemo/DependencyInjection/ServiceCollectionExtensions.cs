using Microsoft.Extensions.DependencyInjection;

namespace RestClientDemo.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddRestClient();

        services.AddSingleton<Program>();

        return services;
    }
}

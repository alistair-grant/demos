using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleAppDependencyInjectionDemo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddLogging(options =>
        {
            options.AddConsole();
        });

        services.AddSingleton<Executor>();

        return services;
    }
}

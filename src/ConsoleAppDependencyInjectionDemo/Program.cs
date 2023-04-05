// Adapted from https://siderite.dev/blog/creating-console-app-with-dependency-injection-in-/

using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppDependencyInjectionDemo;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            await new ServiceCollection()
                .ConfigureServices()
                .BuildServiceProvider()
                .GetRequiredService<Executor>()
                .RunAsync();
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception.Message);
        }
    }
}

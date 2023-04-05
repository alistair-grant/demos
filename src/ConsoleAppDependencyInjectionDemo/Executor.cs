using Microsoft.Extensions.Logging;

namespace ConsoleAppDependencyInjectionDemo
{
    public class Executor
    {
        private readonly ILogger _logger;

        public Executor(ILogger<Executor> logger)
        {
            _logger = logger;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Starting executor...");

            await Task.Delay(TimeSpan.FromSeconds(2));

            _logger.LogInformation("Stopping executor...");

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}

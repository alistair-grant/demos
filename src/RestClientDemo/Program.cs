using Microsoft.Extensions.DependencyInjection;
using RestClientDemo.DependencyInjection;
using RestClientDemo.RestClients;

public class Program
{
	private readonly IRestClient _restClient;

    public Program(IRestClient restClient)
    {
		_restClient = restClient;
    }

    public static async Task Main(string[] args)
    {
		try
		{
			await new ServiceCollection()
				.ConfigureServices()
				.BuildServiceProvider()
				.GetRequiredService<Program>()
				.RunAsync(CancellationToken.None);
		}
		catch (Exception exception)
		{
			Console.Error.WriteLine(exception.Message);
		}
    }

	private async Task RunAsync(CancellationToken cancellationToken)
	{
		const int PageSize = 100;

		var keepFetching = true;
		var startId = 0;

		while (keepFetching)
		{
            var models = await _restClient.GetModelsAsync(startId, PageSize, cancellationToken);

			keepFetching = false;
			foreach (var model in models)
			{
                Console.WriteLine(model);

				keepFetching = true;
				startId = model.Id;
            }

			startId++; // next ID after last model.Id
        }
	}
}
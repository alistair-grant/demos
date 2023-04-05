namespace RestClientDemo.RestClients;

public interface IRestClient
{
    Task<IEnumerable<RestModel>> GetModelsAsync(int startId, int pageSize, CancellationToken cancellationToken);
}

using Microsoft.AspNetCore.WebUtilities;

namespace RestClientDemo.Mocks;

public static class UriExtensions
{
    public static int? ParseQueryParameterAsInt(this Uri uri, string parameterName)
    {
        int? result = null;

        var parameters = QueryHelpers.ParseQuery(uri.Query);
        if (parameters.TryGetValue(parameterName, out var value))
        {
            result = int.Parse(value);
        }

        return result;
    }
}

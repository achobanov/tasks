using System.Net.Http.Json;

namespace Challenge.Common.HTTP;

public abstract class HttpClientBase
{
    private readonly HttpClient client;

    protected HttpClientBase(HttpClient client)
    {
        this.client = client;
    }

    protected async Task Post<T>(string endpoint, T payload, CancellationToken? cancellationToken = null)
    {
        try
        {
            cancellationToken = cancellationToken ?? CancellationToken.None;
            await client.PostAsJsonAsync(endpoint, payload, cancellationToken: cancellationToken.Value);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

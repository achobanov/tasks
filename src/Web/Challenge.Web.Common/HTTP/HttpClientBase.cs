using Challenge.Domain.Core;
using System.Net;
using System.Net.Http.Json;

namespace Challenge.Common.HTTP;

public abstract class HttpClientBase
{
    private readonly HttpClient client;
    private readonly INotifier _notifier;

    protected HttpClientBase(HttpClient client, INotifier notifier)
    {
        this.client = client;
        _notifier = notifier;
    }

    protected async Task Post<T>(string endpoint, T payload, CancellationToken? cancellationToken = null)
    {
        cancellationToken = cancellationToken ?? CancellationToken.None;
        var response = await client.PostAsJsonAsync(endpoint, payload, cancellationToken: cancellationToken.Value);
        await HandleResponse(response);
    }

    private async Task HandleResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }
        var contents = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new DomainException(contents);
        }
        throw new Exception(contents);
    }
}

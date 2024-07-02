using Challenge.Common.JSON;
using Challenge.Domain.Core;
using Challenge.Web.Common.Contracts;
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

    protected async Task<string> Post<T>(string endpoint, T payload, CancellationToken? cancellationToken = null)
    {
        cancellationToken = cancellationToken ?? CancellationToken.None;
        var response = await client.PostAsJsonAsync(endpoint, payload, cancellationToken: cancellationToken.Value);
        return await HandleResponse(response);
    }

    private async Task<string> HandleResponse(HttpResponseMessage response)
    {
        var contents = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return contents;
        }
        if (response.StatusCode != HttpStatusCode.BadRequest)
        {
            throw new Exception(contents);
        }
        var aggregateValidations = contents.FromJson<AggregateValidationContract>();
        if (aggregateValidations != null)
        {
            aggregateValidations.Throw();
        }

        throw new DomainException(contents);
    }
}

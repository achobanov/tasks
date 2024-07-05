using Challenge.Common.JSON;
using Challenge.Domain.Core;
using Challenge.Web.Common.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace Challenge.Common.HTTP;

public abstract class HttpClientBase
{
    private readonly HttpClient _client;
    private readonly INotifier _notifier;

    protected HttpClientBase(HttpClient client, INotifier notifier)
    {
        _client = client;
        _notifier = notifier;
    }

    protected async Task<string> Post<T>(string endpoint, T payload, CancellationToken? cancellationToken = null)
    {
        cancellationToken = cancellationToken ?? CancellationToken.None;
        var response = await _client.PostAsJsonAsync(endpoint, payload, cancellationToken: cancellationToken.Value);
        return await HandleResponse<string>(response);
    }

    protected async Task<T> Get<T>(string endpoint,  CancellationToken? cancellationToken = null)
    {
        var response = await _client.GetAsync(endpoint);
        return await HandleResponse<T>(response);
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<T>();
            if (result == null)
            {
                throw new Exception($"Unable to parse contents to '{typeof(T).Name}'");
            }
            return result;
        }
        var contents = await response.Content.ReadAsStringAsync();
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

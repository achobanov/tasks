using System.Net.Http.Json;
using VL.Challenge.Blazor.Client.Exceptions;

namespace VL.Challenge.Blazor.Client.Services;

public class HttpService : IHttpService
{
    private readonly IToaster _toaster;
    private readonly IHttpClientFactory _httpClientFactory;
    private HttpClient _httpClient => _httpClientFactory.CreateClient();

    public HttpService(IToaster toaster, IHttpClientFactory httpClientFactory)
    {
        _toaster = toaster;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> Delete(string endpoint)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            await CheckResponse(response);
            return true;
        }
        catch (Exception ex)
        {
            HandleError(endpoint, ex);
            return false;
        }
    }

    public async Task<T?> Get<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await GetContents<T>(response);
        }
        catch (Exception ex)
        {
            HandleError(endpoint, ex);
            return default;
        }
    }

    public async Task<T?> Post<T>(string endpoint, object? data = null)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            return await GetContents<T>(response);
        }
        catch (Exception ex)
        {
            HandleError(endpoint, ex);
            return default;
        }
    }

    public async Task<bool> Post(string endpoint, object data)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            await CheckResponse(response);
            return true;
        }
        catch (Exception ex)
        {
            HandleError(endpoint, ex);
            return false;
        }
    }

    public async Task<bool> Put(string endpoint, object data)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            await CheckResponse(response);
            return true;
        }
        catch (Exception ex)
        {
            HandleError(endpoint, ex);
            return false;
        }
    }

    private async Task<T> GetContents<T>(HttpResponseMessage message)
    {
        await CheckResponse(message);
        var result = await message.Content.ReadFromJsonAsync<T>();
        if (result == null)
        {
            throw new Exception($"Content cannot be deserialized to '{typeof(T).Name}': {result}");
        }
        return result;
    }

    private async Task CheckResponse(HttpResponseMessage message)
    {
        if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new ApiValidationException(await message.Content.ReadAsStringAsync());
        }
        if (!message.IsSuccessStatusCode)
        {
            throw new Exception($"{message.StatusCode}: {await message.Content.ReadAsStringAsync()}");
        }
    }

    private void HandleError(string endpoint, Exception ex)
    {
        var color = ex is ApiValidationException
            ? UiColor.Warning
            : UiColor.Danger;
        _toaster.Add(endpoint, ex.Message, color);
    }
}

public interface IHttpService
{
    Task<T?> Get<T>(string endpoint);
    Task<bool> Post(string endpoint, object data);
    Task<T?> Post<T>(string endpoint, object? data = null);
    Task<bool> Put(string endpoint, object data);
    Task<bool> Delete(string endpoint);
}

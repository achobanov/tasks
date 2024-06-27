using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Common.HTTP;

public static class HttpExtensions
{
    private static HttpClientBuilder? _httpClientBuilder;

    public static HttpClientBuilder ConfigureHttp(this IServiceCollection services)
    {
        return _httpClientBuilder ??= new HttpClientBuilder(services);
    }
}

public class HttpClientBuilder
{
    private readonly IServiceCollection _services;

    public HttpClientBuilder(IServiceCollection services)
    {
        services.AddHttpClient();
        _services = services;
    }

    public HttpClientBuilder AddClient<T>(IConfiguration configuration)
        where T : class
    {
        var httpClientConfiguration = configuration
            .GetRequiredSection(typeof(T).Name)
            .Get<HttpClientConfiguration>()
            ?? throw new Exception($"Invalid configuration for client '{typeof(T).Name}'");

        _services.AddHttpClient<T>(client =>
        {
            client.BaseAddress = new Uri(httpClientConfiguration.BaseAddress);
        });

        return this;
    }
}
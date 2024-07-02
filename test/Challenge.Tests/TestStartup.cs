using Castle.Components.DictionaryAdapter.Xml;
using Challenge.API;
using Challenge.Common.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Tests;

public class TestStartup : Startup
{
    public TestStartup(IConfiguration configuration) : base(configuration)
    {
    }

    public void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.RegisterConventionalServices();
    }
}

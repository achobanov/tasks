using Challenge.API;
using Challenge.Common.Injection;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Tests;

public class TestStartup : Startup
{
    public void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.RegisterConventionalServices();
    }
}

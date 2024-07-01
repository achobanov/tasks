using Challenge.Common.HTTP;
using Challenge.Common.Injection;
using Challenge.Web.Client.HTTP;
using Challenge.Web.Client.Toasts;

namespace Challenge.Web.Client;

public static class Startup
{
    public static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureHttp()
            .AddClient<FileUploadClient>(configuration);

        // TODO: use conventional
        services
            .RegisterConventionalServices()
            .AddSingleton<Toaster>()
            .AddSingleton<IToaster>(x => x.GetRequiredService<Toaster>());

        return services;
    }
}

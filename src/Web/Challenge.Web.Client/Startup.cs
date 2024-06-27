using Challenge.Common.HTTP;
using Challenge.Web.Client.Services;

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
            .AddSingleton<Toaster>()
            .AddSingleton<IToaster>(x => x.GetRequiredService<Toaster>());

        return services;
    }
}

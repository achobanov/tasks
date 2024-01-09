using VL.Challenge.API.Services;
using VL.Challenge.API.Redis;

namespace VL.Challenge.API;

public static class ApiInitializer
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<ITasksService, TasksService>();
        services.AddVLCache(configuration);

        return services;
    }
}

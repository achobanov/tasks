using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VL.Challenge.Common.CRUD;
using VL.Challenge.Storage.EFCore;
using VL.Challenge.Storage.Repositories;

namespace VL.Challenge.Storage;

public static class Initializer
{
    public static IServiceCollection AddVLStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConfiguration = new StorageConfiguration(configuration);

        services.AddDbContext<ChallengeDbContext>(opt => opt.UseSqlServer(sqlConfiguration.ConnectionString));
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IRead<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(ICreate<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IUpdate<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IDelete<>), typeof(GenericRepository<>));
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}

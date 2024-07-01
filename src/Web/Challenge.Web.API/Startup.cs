using Challenge.API.Filters;
using Challenge.Common.Injection;

namespace Challenge.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.RegisterConventionalServices();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseWebAssemblyDebugging();
        }

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}

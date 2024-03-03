using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VL.Challenge.Blazor.Client;
using VL.Challenge.Blazor.Client.Services;
using Options = Microsoft.Extensions.Options.Options;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(Options.DefaultName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddSingleton<UserContext>();
builder.Services.AddSingleton<IUserContext>(x => x.GetRequiredService<UserContext>());
builder.Services.AddTransient<IUserApi, UserApi>();
builder.Services.AddTransient<ITaskApi, TaskApi>();
builder.Services.AddSingleton<Toaster>();
builder.Services.AddSingleton<IToaster>(x => x.GetRequiredService<Toaster>());
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();

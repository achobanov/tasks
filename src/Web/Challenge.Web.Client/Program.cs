using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Challenge.Web.Client;
using Challenge.Web.Client.Services;
using Options = Microsoft.Extensions.Options.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(Options.DefaultName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddSingleton<Toaster>();
builder.Services.AddSingleton<IToaster>(x => x.GetRequiredService<Toaster>());

await builder.Build().RunAsync();

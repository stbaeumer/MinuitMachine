using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MinuitMachine.Client;
using MinuitMachine.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Services
builder.Services.AddScoped<CredentialStore>();
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<LocalStorageService>();

await builder.Build().RunAsync();

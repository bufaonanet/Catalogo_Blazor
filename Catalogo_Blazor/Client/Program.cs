using Catalogo_Blazor.Client;
using Catalogo_Blazor.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<TokenAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());

builder.Services.AddScoped<IAuthorizeService, TokenAuthenticationStateProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using CP.Client;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "CPTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<CP.Client.CarParkService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("CP.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CP.Server"));
builder.Services.AddScoped<CP.Client.SecurityService>();
builder.Services.AddScoped<AuthenticationStateProvider, CP.Client.ApplicationAuthenticationStateProvider>();
var host = builder.Build();
await host.RunAsync();
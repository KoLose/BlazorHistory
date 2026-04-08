using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using BlazorApp.ApiRequest;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp  => new HttpClient { BaseAddress = new Uri("http://localhost:5248") });
builder.Services.AddScoped<RequestApi>();

await builder.Build().RunAsync();
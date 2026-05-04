using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using BlazorApp.ApiRequest;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Local
builder.Services.AddScoped(sp  => new HttpClient { BaseAddress = new Uri("http://localhost:5248") });

// Host
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://webapihistory.onrender.com/api/") });

builder.Services.AddScoped<RequestApi>();
builder.Services.AddScoped<CurrentUser>();
await builder.Build().RunAsync();
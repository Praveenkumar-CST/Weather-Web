using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp11;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Use correct backend URL
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5188") });

await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage; // Ensure this is included if you're still using it
using BlazorApp11;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add LocalStorage support (only if needed)
builder.Services.AddBlazoredLocalStorage();

// Register HttpClient properly in DI
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5188/") // Ensure correct API base URL
});

await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using BlazorApp11;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage(); // Add LocalStorage support

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient using DI, ensuring it's correctly configured
builder.Services.AddScoped(sp => new HttpClient 
{
    BaseAddress = new Uri("http://127.0.0.1:5188") // Correct API base URL
});

await builder.Build().RunAsync();

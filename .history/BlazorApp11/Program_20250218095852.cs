using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using BlazorApp11;
using System.Net.Http;
using MongoDB.Driver;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<MongoDBService>(); // Register MongoDB service

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient service for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

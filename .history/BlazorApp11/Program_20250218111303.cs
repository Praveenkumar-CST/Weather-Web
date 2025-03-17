using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using BlazorApp11; // Adjust according to your project namespace
using MongoDB.Driver; // MongoDB namespace

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register services for local storage and MongoDB connection
builder.Services.AddBlazoredLocalStorage();

// Register HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register MongoDB client to be used for interacting with MongoDB from your Blazor app
builder.Services.AddScoped<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"));

// Register your other services if needed (e.g., Weather Service)
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();

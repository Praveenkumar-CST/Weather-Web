using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MongoDB.Driver; // MongoDB namespace
using BlazorApp11; // Adjust according to your project namespace

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register services for local storage and MongoDB connection
builder.Services.AddBlazoredLocalStorage();

// Register MongoDB client for interacting with MongoDB from your Blazor app
builder.Services.AddScoped<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"));

// Register the MongoDB database and collection
builder.Services.AddScoped(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var database = mongoClient.GetDatabase("WeatherAppDB"); // Replace with your DB name
    return database;
});

// Register HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register your other services if needed (e.g., Weather Service)
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();

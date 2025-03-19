using WeatherBackend.Data;
using WeatherBackend.Repositories;
using WeatherBackend.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Services
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddScoped<UserFavoritesRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection(); // 🔹 Redirect HTTP to HTTPS (optional)
app.UseRouting(); // 🔹 Ensure routing is set up
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication(); // 🔹 If authentication is used (optional)
app.UseAuthorization();

app.MapControllers();
app.Run();

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Async method to get weather data
    public async Task<Weather> GetWeatherAsync()
    {
        // Simulating an API call or data retrieval
        await Task.Delay(1000);  // Simulate delay for fetching data

        return new Weather
        {
            Temperature = "25°C",  // Example data
            Condition = "Clear"    // Example data
        };
    }
}

public class Weather
{
    public string Temperature { get; set; } = string.Empty;  // Default initialization
    public string Condition { get; set; } = string.Empty;    // Default initialization
}

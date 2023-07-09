using Plugin.Geolocation.Services;
using Microsoft.Extensions.Configuration;

namespace Plugin.Geolocation;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
    {
        Console.WriteLine($"MauiBundler::ConfigureServices -> Configure Geolocation services");
        
        services.AddScoped<IGeoLocationService, GeoLocationService>();
    }
}
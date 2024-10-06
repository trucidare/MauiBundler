using Microsoft.Extensions.Configuration;
using Plugin.Camera.Services;

namespace Plugin.Camera;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
    {
        Console.WriteLine($"MauiBundler::ConfigureServices -> Configure Camera services");
        
        services.AddScoped<ICameraService, CameraService>();
    }
}
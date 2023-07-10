using Microsoft.Extensions.Configuration;
using Plugin.Device.Services;

namespace Plugin.Device;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
    {
        Console.WriteLine($"MauiBundler::ConfigureServices -> Configure Device services");
        
        services.AddScoped<IDeviceService, DeviceService>();
    }
}
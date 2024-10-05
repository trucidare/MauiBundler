using Microsoft.Extensions.Configuration;
using Plugin.Bluetooth.Services;
using Shiny;
using Shiny.BluetoothLE;

namespace Plugin.Bluetooth;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _, MauiAppBuilder builder)
    {
        Console.WriteLine($"MauiBundler::ConfigureServices -> Configure Bluetooth services");
        // Add shiny ble stuff
        builder.UseShiny();
        builder.Services.AddBluetoothLE();
        
        services.AddSingleton<IBluetoothService, BluetoothService>();
    }
}
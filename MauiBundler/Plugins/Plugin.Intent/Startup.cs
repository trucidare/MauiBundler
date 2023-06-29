using Microsoft.Extensions.Configuration;
using Plugin.Intent.Services;

namespace Plugin.Intent;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
    {
        Console.WriteLine($"MauiBundler::ConfigureServices -> Configure Intent services");
        
        services.AddSingleton<IIntentService, IntentService>();
    }
}
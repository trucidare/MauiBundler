using Microsoft.Extensions.Configuration;
using Plugin.AppActions.Services;

namespace Plugin.AppActions;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
    {
        Console.WriteLine($"MauiBundler::ConfigureServices -> Configure Intent services");
        
        services.AddSingleton<IAppActionsService, AppActionsService>();
    }
}
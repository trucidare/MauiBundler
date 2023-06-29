using Microsoft.Extensions.Configuration;
using Plugin.Filesystem.Services;

namespace Plugin.Filesystem;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
    {
        Console.WriteLine($"MauiBundler::ConfigureServices -> Configure Filesystem services");
        
        services.AddScoped<IFilesystemService, FilesystemService>();
    }
}
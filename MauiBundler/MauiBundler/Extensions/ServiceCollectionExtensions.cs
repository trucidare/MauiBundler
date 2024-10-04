using System.Reflection;
using MauiBundler.Abstractions;
using MauiBundler.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;

namespace MauiBundler.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMauiBundler(this IServiceCollection services, IConfiguration configuration, Assembly anchor)
    {
        services.AddBlazorWebViewDeveloperTools();
        services.AddMauiBlazorWebView();
	    services.AddMauiBundlerInternal(configuration, anchor);

         if (Preferences.Get(Constants.InstallationGuid, null) == null)
            Preferences.Set(Constants.InstallationGuid, Guid.NewGuid().ToString());
    }
}
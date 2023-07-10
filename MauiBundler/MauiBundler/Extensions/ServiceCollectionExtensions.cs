using System.Reflection;
using MauiBundler.Abstractions;
using MauiBundler.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;

namespace MauiBundler.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMauiBundler(this IServiceCollection services, IConfiguration configuration, Assembly anchor)
    {
        services.AddBlazorWebViewDeveloperTools();
        services.AddMauiBlazorWebView();
	    services.AddMauiBundlerInternal(configuration, anchor);

         if (Preferences.Get(Constants.INSTALLATION_GUID, null) == null)
            Preferences.Set(Constants.INSTALLATION_GUID, Guid.NewGuid().ToString());

        return services;
    }
}
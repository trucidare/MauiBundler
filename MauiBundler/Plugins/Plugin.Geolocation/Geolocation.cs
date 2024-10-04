using Microsoft.JSInterop;
using Plugin.Geolocation.Services;
using MauiBundler.Abstractions.Interfaces;
using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Extensions;

namespace Plugin.Geolocation;

[Plugin("GeoLocation.cs.js", Name = "Geolocation")]
public class GeoLocation
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = IPlatformApplication.Current?.Services.GetService<IPluginService>();
        var fs = IPlatformApplication.Current?.Services.GetService<IGeoLocationService>();

        await js.InitializePluginInterop(fs, typeof(GeoLocation));
        
        Console.WriteLine("MauiBundler::Initialize -> Geolocationplugin initialization from interop!");
    }  
}

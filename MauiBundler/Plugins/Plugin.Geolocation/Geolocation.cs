using Plugin.Helper;
using Microsoft.JSInterop;
using Plugin.Filesystem.Services;
using MauiBundler.Abstractions.Interfaces;
using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Extensions;

namespace Plugin.Geolocation;

[Plugin("Plugin.Geolocation.js", Name = "Geolocation")]
public class Geolocation
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = ServiceHelper.GetService<IPluginService>();
        var fs = ServiceHelper.GetService<IGeolocationService>();

        await js.InitializePluginInterop(fs, typeof(Geolocation));
        
        Console.WriteLine("MauiBundler::Initialize -> Geolocationplugin initialization from interop!");
    }  
}

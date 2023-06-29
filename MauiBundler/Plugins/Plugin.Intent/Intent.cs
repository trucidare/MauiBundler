using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.Intent.Services;
using Plugin.Helper;
using MauiBundler.Abstractions.Extensions;
namespace Plugin.Intent;


[Plugin("Plugin.Intent.js", Name = "Intent")]
public class Intent
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = ServiceHelper.GetService<IPluginService>();
        var fs = ServiceHelper.GetService<IIntentService>();

        await js.InitializePluginInterop(fs, typeof(Intent));
        
        Console.WriteLine("MauiBundler::Initialize -> Intentplugin initialization from interop!");
    }  
}

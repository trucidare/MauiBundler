using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.Intent.Services;
using MauiBundler.Abstractions.Extensions;

namespace Plugin.Intent;

[Plugin("Intent.cs.js", Name = "Intent")]
public class Intent
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = IPlatformApplication.Current?.Services.GetService<IPluginService>();
        var fs = IPlatformApplication.Current?.Services.GetService<IIntentService>();

        await js.InitializePluginInterop(fs, typeof(Intent));
        
        Console.WriteLine("MauiBundler::Initialize -> Intentplugin initialization from interop!");
    }  
}

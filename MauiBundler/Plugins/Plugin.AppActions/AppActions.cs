using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Extensions;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.AppActions.Services;

namespace Plugin.AppActions;

[Plugin("AppActions.cs.js", Name = "AppActions")]
public class AppActions
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = IPlatformApplication.Current!.Services.GetRequiredService<IPluginService>();
        var fs = IPlatformApplication.Current!.Services.GetRequiredService<IAppActionsService>();

        await js.InitializePluginInterop(fs, typeof(AppActions));
        
        Console.WriteLine("MauiBundler::Initialize -> Appactionsplugin initialization from interop!");
    }  
}

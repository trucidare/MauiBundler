using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.Intent.Services;
using MauiBundler.Abstractions.Extensions;
namespace Plugin.Intent;


[Plugin("AppActions.cs.js", Name = "AppActions")]
public class AppActions
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = IPlatformApplication.Current?.Services.GetService<IPluginService>();
        var fs = IPlatformApplication.Current?.Services.GetService<IAppActionsService>();

        await js.InitializePluginInterop(fs, typeof(AppActions));
        
        Console.WriteLine("MauiBundler::Initialize -> Appactionsplugin initialization from interop!");
    }  
}

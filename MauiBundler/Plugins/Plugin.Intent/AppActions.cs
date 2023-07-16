using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.Intent.Services;
using Plugin.Helper;
using MauiBundler.Abstractions.Extensions;
namespace Plugin.Intent;


[Plugin("AppActions.cs.js", Name = "AppActions")]
public class AppActions
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = ServiceHelper.GetService<IPluginService>();
        var fs = ServiceHelper.GetService<IAppActionsService>();

        await js.InitializePluginInterop(fs, typeof(AppActions));
        
        Console.WriteLine("MauiBundler::Initialize -> Appactionsplugin initialization from interop!");
    }  
}

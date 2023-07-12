using Plugin.Helper;
using Microsoft.JSInterop;
using MauiBundler.Abstractions.Extensions;
using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Interfaces;
using Plugin.Camera.Serices;

namespace Plugin.Camera;

[Plugin("Camera.cs.js", Name = "Camera")]
public class Camera
{

    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = ServiceHelper.GetService<IPluginService>();
        var fs = ServiceHelper.GetService<ICameraService>();

        await js.InitializePluginInterop(fs, typeof(Camera));
        
        Console.WriteLine("MauiBundler::Initialize -> Cameraplugin initialization from interop!");
    }    
}
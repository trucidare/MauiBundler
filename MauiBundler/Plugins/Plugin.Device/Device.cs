using Plugin.Helper;
using Microsoft.JSInterop;
using Plugin.Device.Services;
using MauiBundler.Abstractions.Extensions;
using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Interfaces;

namespace Plugin.Device;

[Plugin("Device.cs.js", Name = "Device")]
public class Device
{

    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = ServiceHelper.GetService<IPluginService>();
        var fs = ServiceHelper.GetService<IDeviceService>();

        await js.InitializePluginInterop(fs, typeof(Device));
        
        Console.WriteLine("MauiBundler::Initialize -> Deviceplugin initialization from interop!");
    }    
}
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
        var js = IPlatformApplication.Current!.Services.GetRequiredService<IPluginService>();
        var fs = IPlatformApplication.Current!.Services.GetRequiredService<IDeviceService>();

        await js.InitializePluginInterop(fs, typeof(Device));
        
        Console.WriteLine("MauiBundler::Initialize -> Device plugin initialization from interop!");
    }    
}
using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Extensions;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.Bluetooth.Services;

namespace Plugin.Bluetooth;


[Plugin("Bluetooth.cs.js", Name = "Bluetooth")]
public class Bluetooth
{
    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = IPlatformApplication.Current!.Services.GetRequiredService<IPluginService>();
        var fs = IPlatformApplication.Current!.Services.GetRequiredService<IBluetoothService>();

        await js.InitializePluginInterop(fs, typeof(Bluetooth));
        
        Console.WriteLine("MauiBundler::Initialize -> Bluetooth plugin initialization from interop!");
    }  
}

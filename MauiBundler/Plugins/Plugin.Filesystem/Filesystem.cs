using Microsoft.JSInterop;
using Plugin.Filesystem.Services;
using MauiBundler.Abstractions.Extensions;
using MauiBundler.Abstractions.Attributes;
using MauiBundler.Abstractions.Interfaces;

namespace Plugin.Filesystem;

[Plugin("Filesystem.cs.js", Name = "Filesystem")]
public class Filesystem
{

    [JSInvokable("Initialize")]
    public static async Task Initialize()
    {
        var js = IPlatformApplication.Current!.Services.GetRequiredService<IPluginService>();
        var fs = IPlatformApplication.Current!.Services.GetRequiredService<IFilesystemService>();

        await js.InitializePluginInterop(fs, typeof(Filesystem));
        
        Console.WriteLine("MauiBundler::Initialize -> Filesystemplugin initialization from interop!");
    }    
}
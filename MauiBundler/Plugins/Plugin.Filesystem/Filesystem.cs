using Plugin.Helper;
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
        var js = ServiceHelper.GetService<IPluginService>();
        var fs = ServiceHelper.GetService<IFilesystemService>();

        await js.InitializePluginInterop(fs, typeof(Filesystem));
        
        Console.WriteLine("MauiBundler::Initialize -> Filesystemplugin initialization from interop!");
    }    
}
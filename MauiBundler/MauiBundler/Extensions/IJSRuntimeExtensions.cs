using System.Reflection;
using MauiBundler.Abstractions.Attributes;
using Microsoft.JSInterop;

namespace MauiBundler.Extensions;

public static class IjsRuntimeExtensions
{
    public static async Task<IJSRuntime> AddPlugins(this IJSRuntime runtime, Assembly asm)
    {
        foreach (var plugin in FindPluginTypes(asm))
            await TryImportPlugin(runtime, plugin);

        return runtime;
    }


    private static (PluginAttribute Plugin, string Namespace)[] FindPluginTypes(Assembly anchor)
    {
        PluginAttribute attr = null!;
        List<(PluginAttribute, string)> result = new();

        foreach (var f in anchor.GetReferencedAssemblies().Where(s => s.Name!.Contains("Plugin", StringComparison.InvariantCultureIgnoreCase)))
            foreach (var t in Assembly.Load(f).GetTypes())
                if ((attr = t.GetCustomAttribute<PluginAttribute>()!) != null)
                    result.Add((attr, t.Namespace!));


        var refFiles = Directory.GetFiles(Path.GetDirectoryName(anchor.Location) ?? ".")
                .Where(s => s.Contains("Plugin", StringComparison.InvariantCultureIgnoreCase)
                         && s.Contains("dll", StringComparison.InvariantCultureIgnoreCase));

        foreach (var refFile in refFiles)
            foreach (var t in Assembly.LoadFrom(refFile).GetTypes())
                if ((attr = t.GetCustomAttribute<PluginAttribute>()!) != null)
                    result.Add((attr, t.Namespace!));

        return result.ToArray();
    }

    private static async Task TryImportPlugin(IJSRuntime runtime, (PluginAttribute Plugin, string Namespace) plugin)
    {
        try {
            await runtime.InvokeVoidAsync("import", $"/{plugin.Plugin.JavaScriptFile}");
            await runtime.InvokeVoidAsync("window.MauiBundler.Plugins.initializePlugin", plugin.Namespace, "Initialize");
            Console.WriteLine($"MauiBundler::TryImportPlugin -> Imported Plugin {plugin.Namespace}.{plugin.Plugin.Name} - ({plugin.Plugin.JavaScriptFile})");
        }
        catch (Exception ex) {
            Console.WriteLine($"MauiBundler::TryImportPlugin -> Error trying to import Plugin {plugin.Plugin.Name} - ({plugin.Plugin.JavaScriptFile})\r\n{(ex.InnerException ?? ex).Message}");
        }
    }
}
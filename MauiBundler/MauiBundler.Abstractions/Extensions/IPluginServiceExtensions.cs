using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;

namespace MauiBundler.Abstractions.Extensions;

public static class IPluginServiceExtensions
{
    public async static Task<IPluginService> InitializePluginInterop(this IPluginService pluginService, object fs, Type pluginType, string pluginName = "")
    {
        var moduleName = !string.IsNullOrEmpty(pluginName) ? pluginName : pluginType.Name;
        await pluginService.InprocessJSRuntime!.InvokeVoidAsync($"{IPluginService.JSPrefix}.{moduleName}.initialize",pluginType.Namespace,DotNetObjectReference.Create(fs));

        return pluginService;
    }
}
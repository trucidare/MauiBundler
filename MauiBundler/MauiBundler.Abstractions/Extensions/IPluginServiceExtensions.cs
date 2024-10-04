using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;

namespace MauiBundler.Abstractions.Extensions;

public static class PluginServiceExtensions
{
    public static async Task<IPluginService> InitializePluginInterop(this IPluginService pluginService, object fs, Type pluginType, string pluginName = "")
    {
        var moduleName = !string.IsNullOrEmpty(pluginName) ? pluginName : pluginType.Name;
        await pluginService.InprocessJsRuntime!.InvokeVoidAsync($"{IPluginService.JsPrefix}.{moduleName}.initialize",pluginType.Namespace,DotNetObjectReference.Create(fs));

        return pluginService;
    }
}
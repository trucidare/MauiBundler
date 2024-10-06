using System.Reflection;
using Microsoft.JSInterop;
using MauiBundler.Abstractions;
using Microsoft.AspNetCore.Components;
using MauiBundler.Abstractions.Extensions;
using MauiBundler.Abstractions.Interfaces;
using MauiBundler.Extensions;

namespace MauiBundler.Bootstrap;

public class Main : ComponentBase
{
    [Inject]
    private IJSRuntime JsRuntime { get; set; }

    [Inject]
    private IPluginService PluginService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        PluginService.InprocessJsRuntime = JsRuntime;
        
        await JsRuntime.InvokeVoidAsync("import", Constants.JsImportPath);
        if (PluginService.AnchorAssembly != null)
            await JsRuntime.AddPlugins(PluginService.AnchorAssembly);
    }
}
using System.Reflection;
using Microsoft.JSInterop;
using MauiBundler.Abstractions;
using Microsoft.AspNetCore.Components;
using MauiBundler.Abstractions.Extensions;
using MauiBundler.Abstractions.Interfaces;

namespace MauiBundler.Bootstrap;

public partial class Main : ComponentBase
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    [Inject]
    private IPluginService PluginService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        PluginService.InprocessJSRuntime = JSRuntime;
        
        await JSRuntime.InvokeVoidAsync("import", Constants.JS_IMPORT_PATH);
        if (PluginService.AnchorAssembly != null)
            await JSRuntime.AddPlugins(PluginService.AnchorAssembly);
    }
}
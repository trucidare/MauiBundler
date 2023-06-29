using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using System.Reflection;

namespace MauiBundler.Abstractions.Services;

public class PluginService : IPluginService
{
    public IJSRuntime? InprocessJSRuntime { get; set; }
    public Assembly? AnchorAssembly {get;set;}
}

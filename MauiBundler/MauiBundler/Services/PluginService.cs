using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using System.Reflection;

namespace MauiBundler.Services;

public class PluginService : IPluginService
{
    public IJSRuntime? InprocessJsRuntime { get; set; }
    public Assembly? AnchorAssembly {get;set;}
}

using Microsoft.JSInterop;
using System.Reflection;

namespace MauiBundler.Abstractions.Interfaces;

public interface IPluginService
{
    public const string JSPrefix = "window.MauiBundler.Plugins";
    IJSRuntime? InprocessJSRuntime { get; set; }
    Assembly? AnchorAssembly {get;set;  }
}
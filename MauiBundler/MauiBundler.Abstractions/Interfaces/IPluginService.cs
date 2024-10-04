using Microsoft.JSInterop;
using System.Reflection;

namespace MauiBundler.Abstractions.Interfaces;

public interface IPluginService
{
    public const string JsPrefix = "window.MauiBundler.Plugins";
    IJSRuntime? InprocessJsRuntime { get; set; }
    Assembly? AnchorAssembly {get;set;  }
}
using System;
namespace MauiBundler.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PluginAttribute : Attribute
{
    public string? Name { get; set; }
    public string JavaScriptFile { get; set; }

    public PluginAttribute(string javaScriptFile)
            => JavaScriptFile = javaScriptFile;
}


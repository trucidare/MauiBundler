using System;
namespace MauiBundler.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class PluginAttribute(string javaScriptFile) : Attribute
{
    public string? Name { get; set; }
    public string JavaScriptFile { get; set; } = javaScriptFile;
}


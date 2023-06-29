class Plugins {
    
    initializePlugin(pluginNamespace, method) {
        DotNet.invokeMethodAsync(pluginNamespace, method).catch()
    }

};

if (!window.MauiBundler)
    window.MauiBundler = {};

window.MauiBundler.Plugins = new Plugins();
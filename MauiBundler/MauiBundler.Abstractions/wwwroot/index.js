import { PluginBase } from "./core.js";
import { Plugins } from "./plugins.js";
if (!window.MauiBundler)
    window.MauiBundler = {};
MauiBundler.Plugins = new Plugins();
window.PluginBase = PluginBase;

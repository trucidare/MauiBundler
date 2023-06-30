import { PluginBase } from "./core.js";
import { Plugins } from "./plugins.js";

export interface DotnetRef {
  invokeMethodAsync(name: string, ...args: unknown[]): Promise<unknown>;
}

MauiBundler.Plugins = new Plugins() as Plugins & Record<string, PluginBase>;
window.PluginBase = PluginBase;

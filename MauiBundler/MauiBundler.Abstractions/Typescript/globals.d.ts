import { PluginBase as PB } from "./core";
import { DotnetRef } from "./index";
import { Plugins } from "./plugins";

declare global {
  var MauiBundler: {
    Plugins: Plugins & Record<string, PB>;
  };

  var PluginBase: {
    prototype: PB;
    new (): PB;
  };

  var DotNet: DotnetRef;
}

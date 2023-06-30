import type { DotnetRef } from "./index";

export class PluginBase {
  namespace: string;
  dotnetRef: DotnetRef;

  constructor() {
    console.log(`MauiBundler::JS -> Initialized ${this.constructor.name}!`);
  }

  initialize(namespace, dotnetRef) {
    this.namespace = namespace;
    this.dotnetRef = dotnetRef;
  }

  invokeMethodAsync(name, ...args) {
    return this.dotnetRef.invokeMethodAsync(name, ...args);
  }
}

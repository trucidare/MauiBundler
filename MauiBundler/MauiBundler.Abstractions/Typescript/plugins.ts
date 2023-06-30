export class Plugins {
  initializePlugin(pluginNamespace, method) {
    DotNet.invokeMethodAsync(pluginNamespace, method).catch();
  }
}

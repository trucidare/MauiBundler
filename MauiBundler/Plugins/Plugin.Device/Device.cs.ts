class Device extends PluginBase {
    
    async deviceId() {
        return await this.invokeMethodAsync("deviceId");
    }

    async installationId() {
        return await this.invokeMethodAsync("installationId");
    }
}
  
MauiBundler.Plugins.Device = new Device();
  
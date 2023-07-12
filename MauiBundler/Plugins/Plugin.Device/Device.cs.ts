class Device extends PluginBase {
    
    async deviceId() {
        return await this.invokeMethodAsync("deviceId");
    }

    async installationId() {
        return await this.invokeMethodAsync("installationId");
    }

    async readDeviceInfo() {
        return await this.invokeMethodAsync("readDeviceInfo");
    }
}
  
MauiBundler.Plugins.Device = new Device();
  
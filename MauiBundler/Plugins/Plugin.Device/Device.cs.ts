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

    async displayInfo() {
        return await this.invokeMethodAsync("displayInfo")
    }
}
  
MauiBundler.Plugins.Device = new Device();
  
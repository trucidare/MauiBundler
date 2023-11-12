class Camera extends PluginBase {
    async takePhoto() {
        return await this.invokeMethodAsync("takePhoto");
    }
    async pickPhoto() {
        return await this.invokeMethodAsync("pickPhoto");
    }
}
MauiBundler.Plugins.Camera = new Camera();

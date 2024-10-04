class Filesystem extends PluginBase {
    async fileExists(path) {
        return await this.invokeMethodAsync("fileExists", path);
    }
    async writeFile(path, content, append = false) {
        return await this.invokeMethodAsync("writeFile", path, content, append);
    }
    async deleteFile(path) {
        return await this.invokeMethodAsync("deleteFile", path);
    }
    async readFile(path) {
        return await this.invokeMethodAsync("readFile", path);
    }
}
MauiBundler.Plugins.Filesystem = new Filesystem();

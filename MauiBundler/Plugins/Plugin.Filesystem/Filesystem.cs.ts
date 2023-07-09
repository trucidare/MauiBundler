export enum Directories {

}

class Filesystem extends PluginBase {

    async fileExists(path: string) {
        return await this.invokeMethodAsync("fileExists", path);
    }

    async writeFile(path: string, content: string, append: boolean = false) {
        return await this.invokeMethodAsync("writeFile", path, content, append);
    }

    async deleteFile(path: string) {
        return await this.invokeMethodAsync("deleteFile", path);
    }

    async readFile(path: string) {
        return await this.invokeMethodAsync("readFile",path);
    }
    
}
  
MauiBundler.Plugins.Filesystem = new Filesystem();
using Microsoft.JSInterop;

namespace Plugin.Filesystem.Services;

public class FilesystemService : IFilesystemService
{
    [JSInvokable("deleteFile")]
    public async ValueTask<bool> DeleteFile(string path)
    {
        if (await FileExists(path)) {
            File.Delete($"{FileSystem.AppDataDirectory}/{path}");
            return true;
        }

        return false;
    }

    [JSInvokable("fileExists")]
    public async ValueTask<bool> FileExists(string path)
    {
        return await ValueTask.FromResult(File.Exists($"{FileSystem.AppDataDirectory}/{path}"));
    }

    [JSInvokable("readFile")]
    public async ValueTask<string> ReadFile(string path)
    {
        return await File.ReadAllTextAsync($"{FileSystem.AppDataDirectory}/{path}");
    }

    [JSInvokable("writeFile")]
    public async ValueTask<bool> WriteFile(string path, string content, bool append)
    {
        await File.WriteAllTextAsync($"{FileSystem.AppDataDirectory}/{path}", content);
        return true;
    }
}
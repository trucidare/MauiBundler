using Microsoft.JSInterop;

namespace Plugin.Filesystem.Services;


public class FilesystemService : IFilesystemService
{
    [JSInvokable("deleteFile")]
    public async ValueTask<bool> DeleteFile(string path)
    {
        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        if (await FileExists(path)) 
        {    
            File.Delete($"{docsDirectory}/{path}");
            return true;
        }

        return false;
    }

    [JSInvokable("fileExists")]
    public async ValueTask<bool> FileExists(string path)
    {
        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        return await ValueTask.FromResult(File.Exists($"{docsDirectory}/{path}"));
    }

    [JSInvokable("readFile")]
    public async ValueTask<string> ReadFile(string path)
    {
        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        return await File.ReadAllTextAsync($"{docsDirectory}/{path}");
    }

    [JSInvokable("writeFile")]
    public async ValueTask<bool> WriteFile(string path, string content, bool append)
    {
        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        await File.WriteAllTextAsync($"{docsDirectory}/{path}", content);
        return true;
    }

}
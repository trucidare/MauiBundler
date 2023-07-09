namespace Plugin.Filesystem.Services;

public interface IFilesystemService
{
    ValueTask<bool> FileExists(string path);
    ValueTask<bool> WriteFile(string path, string content, bool append);
    ValueTask<bool> DeleteFile(string path);
    ValueTask<string> ReadFile(string path);
}

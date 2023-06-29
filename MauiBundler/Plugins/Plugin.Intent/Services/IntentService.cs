namespace Plugin.Intent.Services;

public interface IIntentService
{
    void AddIntentFilter(string category, string action);
    Task PublishIntent(string action, string content);
}
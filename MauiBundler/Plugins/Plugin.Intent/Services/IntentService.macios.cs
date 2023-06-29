namespace Plugin.Intent.Services;

public class IntentService : IIntentService
{
    public void AddIntentFilter(string category, string action)
    {
        throw new NotSupportedException();
    }

    public Task PublishIntent(string action, string content)
    {
        throw new NotSupportedException();
    }
}
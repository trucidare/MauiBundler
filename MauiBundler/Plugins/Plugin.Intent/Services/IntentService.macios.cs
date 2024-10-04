using Microsoft.JSInterop;

namespace Plugin.Intent.Services;

public class IntentService : IIntentService
{
    [JSInvokable("addIntentFilter")]
    public void AddIntentFilter(string category, string action)
    {
        throw new NotImplementedException();
    }

    public Task PublishIntent(string action, string content)
    {
        throw new NotImplementedException();
    }
}


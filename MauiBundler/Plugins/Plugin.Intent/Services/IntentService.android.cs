using Android.Content;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.Intent.Extensions;

namespace Plugin.Intent.Services;

public class IntentService(IPluginService pluginService) : IIntentService
{
    [JSInvokable("addIntentFilter")]
    public void AddIntentFilter(string category, string action)
    {
        if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(action))
        {
            IntentFilter filter = new();
            filter.AddCategory(category);
            filter.AddAction(action);
            MauiApplication.Current.RegisterReceiver(new IntentReceiver(), filter);
        }
    }

    public async Task PublishIntent(string action, string content)
        => await pluginService.InprocessJsRuntime!.InvokeVoidAsync($"MauiBundler.Plugins.{nameof(Intent)}.publishIntent",
                action, content);
}

public class IntentReceiver : BroadcastReceiver
{
    readonly IIntentService _intentService = IPlatformApplication.Current?.Services.GetService<IIntentService>()!;

    public override void OnReceive(Context? context, Android.Content.Intent? intent)
        => _intentService.PublishIntent(intent?.Action!, intent?.JsonFromIntent()!);
}

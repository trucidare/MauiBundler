using System.Text.Json;
using MauiBundler.Abstractions.Interfaces;
using Plugin.Intent.Services.Extensions;
using UIKit;
using Microsoft.JSInterop;
using AppAction = Plugin.Intent.Models.AppAction;

namespace Plugin.Intent.Services;

public sealed class AppActionsService : IAppActionsService, IPlatformAppActions
{
    private readonly IPluginService _jsRuntime = IPlatformApplication.Current?.Services.GetService<IPluginService>()!;

    public void AddAppAction(string id, string title, string subtitle, string icon)
    {
        //https://github.com/lytico/maui/blob/lytico/gtk-ongoing/src/Essentials/src/AppActions/AppActions.android.cs
        List<AppAction> actions = new()
        {
            new AppAction("battery_info", "Battery Info")
        };

        UIApplication.SharedApplication.ShortcutItems = actions.Select(a => a.ToShortcutItem()).ToArray();
    }

    public async Task AppActionCalled(string id, string content)
        => await _jsRuntime.InprocessJsRuntime!.InvokeVoidAsync(
            $"MauiBundler.Plugins.{nameof(AppActions)}.appActionCalled", id, content);

    public void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
    {
        if (shortcutItem.Type == IntentExtensions.ShortcutType)
        {
            var appAction = shortcutItem.ToAppAction();
            Task.Run(async () => await AppActionCalled(appAction.Id, JsonSerializer.Serialize(appAction)));
        }
    }
}
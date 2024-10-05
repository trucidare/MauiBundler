using System.Text.Json;
using MauiBundler.Abstractions.Interfaces;
using UIKit;
using Plugin.AppActions.Services.Extensions;
using Microsoft.JSInterop;
using AppActionsExtensions = Plugin.AppActions.Services.Extensions.AppActionsExtensions;
using AppAction = Plugin.AppActions.Models.AppAction;

namespace Plugin.AppActions.Services;

public sealed class AppActionsService : IAppActionsService, IPlatformAppActions
{
    private readonly IPluginService _jsRuntime = IPlatformApplication.Current?.Services.GetService<IPluginService>()!;

    [JSInvokable("addAppAction")]
    public void AddAppAction(string id, string title, string subtitle, string icon)
    {
        //https://github.com/lytico/maui/blob/lytico/gtk-ongoing/src/Essentials/src/AppActions/AppActions.android.cs
        List<AppAction> actions = new()
        {
            new AppAction(id, title, subtitle, icon)
        };

        UIApplication.SharedApplication.ShortcutItems = actions.Select(a => a.ToShortcutItem()).ToArray();
    }

    public async Task AppActionCalled(string id, string content)
        => await _jsRuntime.InprocessJsRuntime!.InvokeVoidAsync(
            $"MauiBundler.Plugins.{nameof(AppActions)}.appActionCalled", id, content);

    public void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
    {
        if (shortcutItem.Type == AppActionsExtensions.ShortcutType)
        {
            var appAction = shortcutItem.ToAppAction();
            Task.Run(async () => await AppActionCalled(appAction.Id, JsonSerializer.Serialize(appAction)));
        }
    }
}
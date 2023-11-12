using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Plugin.Intent.Services.Extensions;
using AppAction = Plugin.Intent.Models.AppAction;

namespace Plugin.Intent.Services;

public sealed class AppActionsService : IAppActionsService
{
    public void AddAppAction(string id, string title, string subtitle, string icon)
    {
#if __ANDROID_25__
        if (Android.App.Application.Context.GetSystemService(Context.ShortcutService) is not ShortcutManager manager)
            throw new FeatureNotSupportedException();

        List<AppAction> actions = new()
                    {
                        new AppAction("battery_info", "Battery Info")
                    };
#pragma warning disable CA1416 // Known false positive with lambda
        using var list = new JavaList<ShortcutInfo>(actions.Select(a => a.ToShortcutInfo()));
#pragma warning disable CA1416
        manager.SetDynamicShortcuts(list);
#endif
    }

    public Task AppActionCalled(string id, string content)
    {
        throw new NotImplementedException();
    }
}
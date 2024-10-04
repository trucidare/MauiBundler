using Foundation;
using UIKit;
using AppAction = Plugin.Intent.Models.AppAction;

namespace Plugin.Intent.Services.Extensions;


public static class IntentExtensions
{
    public const string ShortcutType = "XE_APP_ACTION_TYPE";

    public static UIApplicationShortcutItem ToShortcutItem(this AppAction action)
    {
        var keys = new List<NSString>();
        var values = new List<NSObject>();

        keys.Add((NSString)"id");
        values.Add((NSString)action.Id);

        if (!string.IsNullOrEmpty(action.Icon))
        {
            keys.Add((NSString)"icon");
            values.Add((NSString)action.Icon);
        }

        return new UIApplicationShortcutItem(
            ShortcutType,
            action.Title,
            action.Subtitle,
            action.Icon != null ? UIApplicationShortcutIcon.FromTemplateImageName(action.Icon) : null,
            new NSDictionary<NSString, NSObject>(keys.ToArray(), values.ToArray()));
    }

    public static AppAction ToAppAction(this UIApplicationShortcutItem shortcutItem)
    {
        string id = null!;
        if (shortcutItem.UserInfo!.TryGetValue((NSString)"id", out var idObj))
            id = idObj?.ToString()!;

        string icon = null!;
        if (shortcutItem.UserInfo.TryGetValue((NSString)"icon", out var iconObj))
            icon = iconObj?.ToString()!;

        return new AppAction(id, shortcutItem.LocalizedTitle, shortcutItem.LocalizedSubtitle, icon);
    }
}

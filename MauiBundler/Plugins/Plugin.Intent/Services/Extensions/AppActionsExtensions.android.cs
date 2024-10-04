using System.Runtime.Versioning;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using AppAction = Plugin.Intent.Models.AppAction;

namespace Plugin.Intent.Services.Extensions;

static partial class AppActionsExtensions
{
    public const string IntentAction = "ACTION_XE_APP_ACTION";

    [SupportedOSPlatform("android25.0")]
    internal static AppAction ToAppAction(this ShortcutInfo shortcutInfo) =>
        new AppAction(shortcutInfo.Id, shortcutInfo.ShortLabel!, shortcutInfo.LongLabel);

    const string extraAppActionId = "EXTRA_XE_APP_ACTION_ID";
    const string extraAppActionTitle = "EXTRA_XE_APP_ACTION_TITLE";
    const string extraAppActionSubtitle = "EXTRA_XE_APP_ACTION_SUBTITLE";
    const string extraAppActionIcon = "EXTRA_XE_APP_ACTION_ICON";

    internal static AppAction ToAppAction(this Android.Content.Intent intent)
        => new AppAction(
            intent.GetStringExtra(extraAppActionId)!,
            intent.GetStringExtra(extraAppActionTitle)!,
            intent.GetStringExtra(extraAppActionSubtitle),
            intent.GetStringExtra(extraAppActionIcon));

    [SupportedOSPlatform("android25.0")]
    internal static ShortcutInfo ToShortcutInfo(this AppAction action)
    {
        var context = Android.App.Application.Context;

        var shortcut = new ShortcutInfo.Builder(context, action.Id)
            .SetShortLabel(action.Title);

        if (!string.IsNullOrWhiteSpace(action.Subtitle))
        {
            shortcut.SetLongLabel(action.Subtitle);
        }

        if (!string.IsNullOrWhiteSpace(action.Icon))
        {
            var iconResId = context.Resources?.GetIdentifier(action.Icon, "drawable", context.PackageName);

            shortcut.SetIcon(Icon.CreateWithResource(context, iconResId ?? 0));
        }

        var intent = new Android.Content.Intent(IntentAction);
        intent.SetPackage(context.PackageName);
        intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
        intent.PutExtra(extraAppActionId, action.Id);
        intent.PutExtra(extraAppActionTitle, action.Title);
        intent.PutExtra(extraAppActionSubtitle, action.Subtitle);
        intent.PutExtra(extraAppActionIcon, action.Icon);

        shortcut.SetIntent(intent);

        return shortcut.Build();
    }
}
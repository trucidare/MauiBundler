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
        new(shortcutInfo.Id, shortcutInfo.ShortLabel!, shortcutInfo.LongLabel);

    private const string ExtraAppActionId = "EXTRA_XE_APP_ACTION_ID";
    private const string ExtraAppActionTitle = "EXTRA_XE_APP_ACTION_TITLE";
    private const string ExtraAppActionSubtitle = "EXTRA_XE_APP_ACTION_SUBTITLE";
    private const string ExtraAppActionIcon = "EXTRA_XE_APP_ACTION_ICON";

    internal static AppAction ToAppAction(this Android.Content.Intent intent)
        => new AppAction(
            intent.GetStringExtra(ExtraAppActionId)!,
            intent.GetStringExtra(ExtraAppActionTitle)!,
            intent.GetStringExtra(ExtraAppActionSubtitle),
            intent.GetStringExtra(ExtraAppActionIcon));

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
        intent.PutExtra(ExtraAppActionId, action.Id);
        intent.PutExtra(ExtraAppActionTitle, action.Title);
        intent.PutExtra(ExtraAppActionSubtitle, action.Subtitle);
        intent.PutExtra(ExtraAppActionIcon, action.Icon);

        shortcut.SetIntent(intent);

        return shortcut.Build();
    }
}
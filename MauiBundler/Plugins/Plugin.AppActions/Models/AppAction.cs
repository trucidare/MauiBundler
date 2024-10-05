namespace Plugin.AppActions.Models;

public class AppAction(string id, string title, string? subtitle = null, string? icon = null)
{
    public string Title { get; set; } = title ?? throw new ArgumentNullException(nameof(title));

    public string? Subtitle { get; set; } = subtitle;

    public string Id { get; set; } = id ?? throw new ArgumentNullException(nameof(id));

    internal string? Icon { get; set; } = icon;
}

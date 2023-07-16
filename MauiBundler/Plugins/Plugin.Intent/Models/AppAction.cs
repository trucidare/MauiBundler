namespace Plugin.Intent.Models;

public class AppAction
{
    public AppAction(string id, string title, string? subtitle = null, string? icon = null)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Title = title ?? throw new ArgumentNullException(nameof(title));

        Subtitle = subtitle;
        Icon = icon;
    }

    public string Title { get; set; }

    public string? Subtitle { get; set; }

    public string Id { get; set; }

    internal string? Icon { get; set; }
}

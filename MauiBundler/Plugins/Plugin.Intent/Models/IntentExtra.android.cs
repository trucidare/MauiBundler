using System.Text.Json.Nodes;
using Android.Content;

namespace Plugin.Intent.Models;

public class IntentExtra
{
    public string? Type { get; set; }
    public string? Data { get; set; }
    public string[]? Categories { get; init; }
    public string? Action { get; set; }
    public string? Component { get; set; }
    public string? Package { get; set; }
    public ActivityFlags Flags { get; set; }
    public string? Extras { get; set; }
    public JsonObject[]? ClipData { get; set; }
}
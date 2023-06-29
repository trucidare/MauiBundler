using Android.OS;
using System.Text.Json;
using Plugin.Intent.Models;
using System.Text.Json.Nodes;

namespace Plugin.Intent.Extensions;
public static class IntentExtensions
{
    private static JsonElement ToJson(Bundle bundle, string key)
    {
        if (bundle.Get(key)?.Class.Name == "java.lang.String")
            return JsonSerializer.SerializeToElement(bundle.GetString(key));
        else if (bundle.Get(key)?.Class.Name == "java.lang.Long")
            return JsonSerializer.SerializeToElement(bundle.GetLong(key).ToString());
        else if (bundle.Get(key)?.Class.Name == "java.util.ArrayList") // FIXME: How to determin which content type
            return JsonSerializer.SerializeToElement("");

        return JsonSerializer.SerializeToElement(bundle.GetString(key));
    }

    public static string JsonFromIntent(this Android.Content.Intent intent)
    {
        if (intent == null)
            return "{}";

        var items = new List<JsonObject>();

        if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
        {
            if (intent.ClipData != null)
            {
                for (var i = 0; i < intent.ClipData.ItemCount; i++)
                {
                    var item = intent.ClipData.GetItemAt(i);
                    var js = new JsonObject();
                    try
                    {
                        js.Add(new KeyValuePair<string, JsonNode>("htmlText", JsonNode.Parse((item?.HtmlText ?? "")!)!)!);
                        js.Add(new KeyValuePair<string, JsonNode>("intent", JsonNode.Parse(JsonFromIntent(item?.Intent!) ?? "")!)!);
                        js.Add(new KeyValuePair<string, JsonNode>("text", (item?.Text ?? "")!)!);
                        js.Add(new KeyValuePair<string, JsonNode>("uri", item?.Uri?.ToString()!)!);

                        items.Add(js);
                    }
                    catch
                    {

                    }
                }
            }
        }

        Dictionary<string, object> vals = new();
        foreach (var key in intent.Extras!.KeySet()!)
            vals.Add(key, ToJson(intent.Extras, key));


        var k = new IntentExtra
        {
            Type = intent.Type,
            Data = intent.Data?.ToString(),
            Categories = intent.Categories?.ToArray(),
            Action = intent.Action,
            Flags = intent.Flags,
            Component = intent.Component?.FlattenToString(),
            Package = intent.Package,
            Extras = JsonSerializer.Serialize(vals),
            ClipData = items.ToArray()
        };

        return JsonSerializer.Serialize(k, new JsonSerializerOptions {  
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });
    }
}
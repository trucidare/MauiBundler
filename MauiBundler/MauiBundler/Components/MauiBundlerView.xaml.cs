using System.Reflection;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.FileProviders;

namespace MauiBundler.Components;

public class FilesBlazorWebView : BlazorWebView
{
		public override IFileProvider CreateFileProvider(string contentRootDir)
		{
            try {
                if (Directory.Exists(Path.Combine(FileSystem.AppDataDirectory, contentRootDir))) {
                    Console.WriteLine($"OTA: Use AppDirectoryPath");
                    var inMemoryFiles = new PhysicalFileProvider(Path.Combine(FileSystem.AppDataDirectory, contentRootDir));
                    return new CompositeFileProvider(inMemoryFiles, base.CreateFileProvider(contentRootDir));
                }
                return base.CreateFileProvider(contentRootDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OTA Preparation: {ex.StackTrace}");
                return null!;
            }
		}
	}

public partial class MauiBundlerView : ContentPage
{
	private void ContentPage_Loaded(object sender, EventArgs e)
    {
    }

	public MauiBundlerView()
	{
		InitializeComponent();
        FilesBlazorWebView customBlazorWebView = new FilesBlazorWebView();
        #if ANDROID
            CopyFilesToAppDirectory();
        #endif
    }

    private string[] ListFiles(string root) 
    {
        var result = new List<string>();
        foreach (var s in Platform.AppContext.Assets.List(root))
        {
            var items = ListFiles($"{root}/{s}");
            if (items.Length == 0)
                result.Add($"{root}/{s}");
            else 
                result.AddRange(items);
        }

        return result.ToArray();
    }

    private void CopyFilesToAppDirectory()
    {
        foreach (var s in ListFiles("wwwroot"))
        {
            if (FileSystem.Current.AppPackageFileExistsAsync(s).ConfigureAwait(false).GetAwaiter().GetResult()) 
            {
                using Stream fileStream = FileSystem.Current.OpenAppPackageFileAsync(s).ConfigureAwait(false).GetAwaiter().GetResult();

                var destPath = $"{FileSystem.AppDataDirectory}/{s}";
                var p = Path.GetDirectoryName(destPath);

                if (!Directory.Exists(p))
                    Directory.CreateDirectory(p);

                using FileStream outputStream = File.Create(destPath);
                fileStream.CopyTo(outputStream);
            }
        }
    }

    private void BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e) 
    {   
#if IOS || MACCATALYST
        if ((DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst && DeviceInfo.Current.Version >= new Version(13, 3)) ||
            (DeviceInfo.Current.Platform == DevicePlatform.iOS && DeviceInfo.Current.Version >= new Version(16, 4)))
        {
            e.WebView.SetValueForKey(Foundation.NSObject.FromObject(true), new Foundation.NSString("inspectable"));
        }
#endif
    }
}

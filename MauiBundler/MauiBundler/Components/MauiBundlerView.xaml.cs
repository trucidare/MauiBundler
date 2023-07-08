using System.Reflection;
using Microsoft.AspNetCore.Components.WebView;

namespace MauiBundler.Components;

public partial class MauiBundlerView : ContentPage
{
	
	public MauiBundlerView()
	{
		InitializeComponent();
		mauiWebView.BlazorWebViewInitialized += BlazorWebViewInitialized;
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

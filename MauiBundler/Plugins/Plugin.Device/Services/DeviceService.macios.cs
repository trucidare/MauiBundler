using MauiBundler.Abstractions;
using Microsoft.JSInterop;
using UIKit;

namespace Plugin.Device.Services;

public class DeviceService : IDeviceService
{
    [JSInvokable("installationId")]
    public string? InstallationId() =>  Preferences.Get(Constants.INSTALLATION_GUID, null);
    
    [JSInvokable("deviceId")]
    public string? DeviceId()
    {
        return UIDevice.CurrentDevice.IdentifierForVendor?.ToString();
    }
}
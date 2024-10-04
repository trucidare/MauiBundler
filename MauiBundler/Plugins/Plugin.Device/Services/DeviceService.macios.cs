using MauiBundler.Abstractions;
using Microsoft.JSInterop;
using UIKit;

namespace Plugin.Device.Services;

public class DeviceService : IDeviceService
{
    [JSInvokable("installationId")]
    public string? InstallationId() => Preferences.Get(Constants.InstallationGuid, null);

    [JSInvokable("deviceId")]
    public string? DeviceId()
        => UIDevice.CurrentDevice.IdentifierForVendor?.ToString();
    
    [JSInvokable("readDeviceInfo")]
    public IDeviceInfo ReadDeviceInfo()
        => DeviceInfo.Current;

    [JSInvokable("displayInfo")]
    public IDeviceDisplay DisplayInfo()
        => DeviceDisplay.Current;
}
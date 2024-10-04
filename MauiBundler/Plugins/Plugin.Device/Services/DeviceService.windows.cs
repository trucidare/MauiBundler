using Microsoft.JSInterop;
using MauiBundler.Abstractions;

namespace Plugin.Device.Services;

public class DeviceService : IDeviceService
{
    [JSInvokable("installationId")]
    public string? InstallationId() 
        => Preferences.Get(Constants.INSTALLATION_GUID, null);

    [JSInvokable("deviceId")]
    public string? DeviceId()
        => Windows.System.Profile.SystemIdentification.GetSystemIdForPublisher()?.Id?.ToString();
    
    [JSInvokable("readDeviceInfo")]
    public IDeviceInfo ReadDeviceInfo()
        => DeviceInfo.Current;

    [JSInvokable("displayInfo")]
    public IDeviceDisplay DisplayInfo()
        => DeviceDisplay.Current;
}
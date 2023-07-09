using Microsoft.JSInterop;
using MauiBundler.Abstractions;

namespace Plugin.Device.Services;

public class DeviceService : IDeviceService
{

     [JSInvokable("installationId")]
    public string InstallationId() =>  Preferences.Get(Constants.kInstallationGuid, null) ?? "N/A";

    [JSInvokable("deviceId")]
    public string DeviceId()
    {
         return Windows.System.Profile.SystemIdentification.GetSystemIdForPublisher()?.Id?.ToString() ?? "N/A";
    }
}
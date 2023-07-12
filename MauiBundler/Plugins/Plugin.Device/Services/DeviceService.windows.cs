using Microsoft.JSInterop;
using MauiBundler.Abstractions;

namespace Plugin.Device.Services;

public class DeviceService : IDeviceService
{

     [JSInvokable("installationId")]
     public string? InstallationId() => Preferences.Get(Constants.INSTALLATION_GUID, null);

     [JSInvokable("deviceId")]
     public string? DeviceId()
     {
          return Windows.System.Profile.SystemIdentification.GetSystemIdForPublisher()?.Id?.ToString();
     }
     
     [JSInvokable("readDeviceInfo")]
     public IDeviceInfo ReadDeviceInfo()
     {
          return DeviceInfo.Current;
     }

     [JSInvokable("displayInfo")]
     public IDeviceDisplay DisplayInfo()
     {
          return DeviceDisplay.Current;
     }
}
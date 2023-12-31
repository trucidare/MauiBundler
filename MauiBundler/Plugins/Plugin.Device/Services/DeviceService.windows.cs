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
}
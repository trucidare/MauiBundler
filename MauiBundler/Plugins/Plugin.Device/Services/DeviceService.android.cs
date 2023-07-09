using MauiBundler.Abstractions;
using Microsoft.JSInterop;
using static Android.Provider.Settings;

namespace Plugin.Device.Services;

public class DeviceService : IDeviceService
{
     [JSInvokable("installationId")]
    public string InstallationId() =>  Preferences.Get(Constants.kInstallationGuid, null) ?? "N/A";
    
    [JSInvokable("deviceId")]
    public string DeviceId()
    {
        var context = Android.App.Application.Context;
        string id = Secure.GetString(context.ContentResolver, Secure.AndroidId) ?? "N/A";
        return id;
    }
}
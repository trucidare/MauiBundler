using MauiBundler.Abstractions;
using Microsoft.JSInterop;
using static Android.Provider.Settings;

namespace Plugin.Device.Services;

public class DeviceService : IDeviceService
{
    [JSInvokable("installationId")]
    public string? InstallationId()
        => Preferences.Get(Constants.InstallationGuid, null);

    [JSInvokable("deviceId")]
    public string? DeviceId()
    {
        var context = Android.App.Application.Context;
        return Secure.GetString(context.ContentResolver, Secure.AndroidId);
    }

    [JSInvokable("readDeviceInfo")]
    public IDeviceInfo ReadDeviceInfo()
        => DeviceInfo.Current;

    [JSInvokable("displayInfo")]
    public IDeviceDisplay DisplayInfo()
        => DeviceDisplay.Current;
}
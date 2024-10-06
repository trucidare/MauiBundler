namespace Plugin.Device.Services;

public interface IDeviceService
{
    string? DeviceId();
    string? InstallationId();
    IDeviceInfo ReadDeviceInfo();
    IDeviceDisplay DisplayInfo();
    void WatchBattery();
}

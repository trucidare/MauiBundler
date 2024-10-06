using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;

namespace Plugin.Device.Services;

public partial class DeviceService(IPluginService pluginService)
{
    [JSInvokable("watchBattery")]
    public void WatchBattery()
    {
        Battery.Default.BatteryInfoChanged += OnBatteryInfoChanged;
    }

    private void OnBatteryInfoChanged(object? sender, BatteryInfoChangedEventArgs e)
    {
        BatteryStateChanged(e);
    }

    private async void BatteryStateChanged(object info)
        => await pluginService.InprocessJsRuntime!.InvokeVoidAsync(
            $"MauiBundler.Plugins.{nameof(Device)}.batteryStateChanged", info);
}
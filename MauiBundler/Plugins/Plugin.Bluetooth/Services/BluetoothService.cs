using System.Reactive.Linq;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Shiny;
using Shiny.BluetoothLE;
using Shiny.BluetoothLE.Managed;

namespace Plugin.Bluetooth.Services;

public class BluetoothService(IBleManager bleManager) : IBluetoothService
{
    private readonly IPluginService _jsRuntime = IPlatformApplication.Current?.Services.GetService<IPluginService>()!;
    private IManagedScan? _scanner;
    
    [JSInvokable("scanForDevices")]
    public async Task ScanForDevices()
    {
        var access = await bleManager.RequestAccess();
        if (access == AccessState.Available)
        {
            _scanner = bleManager.CreateManagedScanner();
            _scanner.Peripherals.CollectionChanged += (sender, args) =>
            {
                if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (var item in args.NewItems!)
                        if (item is ManagedScanResult scanResult)
                            DeviceFound(scanResult);
                }
            };
            
            await _scanner.Start();
        }
    }

    [JSInvokable("stopDeviceScan")]
    public async Task StopDeviceScan()
    {
        _scanner?.Stop();
        _scanner?.Dispose();
        await Task.CompletedTask;
    }
    
    private async void DeviceFound(ManagedScanResult device)
        => await _jsRuntime.InprocessJsRuntime!.InvokeVoidAsync(
            $"MauiBundler.Plugins.{nameof(Bluetooth)}.deviceFound", device);
}
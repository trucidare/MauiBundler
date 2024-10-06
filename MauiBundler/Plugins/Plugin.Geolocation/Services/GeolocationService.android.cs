using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;

namespace Plugin.Geolocation.Services;

public class GeoLocationService : Java.Lang.Object, IGeoLocationService, ILocationListener
{
    private readonly IPluginService _pluginService;
    private readonly LocationManager? _locationManager;

    public GeoLocationService(IPluginService pluginService)
    {
        _pluginService = pluginService;
        _locationManager ??= (LocationManager)MauiApplication.Current.GetSystemService(Context.LocationService)!;
    }

    [JSInvokable("start")]
    public void Start()
    {
        if (_locationManager != null)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    PublishStatusChangedEvent("Permission for location is not granted, we can't get location updates")
                        .GetAwaiter().GetResult();
                    return;
                }

                if (!_locationManager.IsLocationEnabled)
                {
                    PublishStatusChangedEvent("Location is not enabled, we can't get location updates").GetAwaiter()
                        .GetResult();
                    return;
                }

                if (!_locationManager.IsProviderEnabled(LocationManager.GpsProvider))
                {
                    PublishStatusChangedEvent("GPS Provider is not enabled, we can't get location updates").GetAwaiter()
                        .GetResult();
                    return;
                }

                _locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 800, 1, this);
            });

        }
    }

    [JSInvokable("stop")]
    public void Stop()
        => _locationManager?.RemoveUpdates(this);

    public void OnLocationChanged(Android.Locations.Location? location)
    {
        if (location != null)
        {
            var loc = new Models.Location(location.Latitude, location.Longitude, location.Bearing, location.Altitude,
                location.Accuracy);
            Task.Run(async () =>
                await _pluginService.InprocessJsRuntime!.InvokeVoidAsync(
                    $"MauiBundler.Plugins.{nameof(GeoLocation)}.locationChanged", loc));
        }
    }

    public void OnProviderDisabled(string provider)
        => Task.Run(async () => await PublishStatusChangedEvent($"{provider} has been disabled"));

    public void OnProviderEnabled(string provider)
        => Task.Run(async () => await PublishStatusChangedEvent($"{provider} now enabled"));

    public void OnStatusChanged(string? provider, [GeneratedEnum] Availability status, Bundle? extras)
        => Task.Run(async () => await PublishStatusChangedEvent($"{provider} change his status and now it's {status}"));

    private async Task PublishStatusChangedEvent(string message)
        => await _pluginService.InprocessJsRuntime!.InvokeVoidAsync(
            $"MauiBundler.Plugins.{nameof(GeoLocation)}.statusChanged", message);

}
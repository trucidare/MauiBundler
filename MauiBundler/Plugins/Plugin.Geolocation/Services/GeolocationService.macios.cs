using CoreLocation;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;

namespace Plugin.Geolocation.Services;

public class GeoLocationService : IGeoLocationService
{
    private readonly IPluginService _jsRuntime = IPlatformApplication.Current?.Services.GetService<IPluginService>()!;

    private readonly CLLocationManager _iosLocationManager;

    public GeoLocationService()
    {
        _iosLocationManager ??= new CLLocationManager()
        {
            DesiredAccuracy = CLLocation.AccuracyBest,
            DistanceFilter = CLLocationDistance.FilterNone,
            PausesLocationUpdatesAutomatically = false
        };
    }

    [JSInvokable("start")]
    public void Start()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                Task.Run(async () =>
                        await PublishStatusChangedEvent(
                            "Permission for location is not granted, we can't get location updates"))
                    .GetAwaiter()
                    .GetResult();

                return;
            }

            _iosLocationManager.RequestAlwaysAuthorization();
            _iosLocationManager.LocationsUpdated += LocationsUpdated;
            _iosLocationManager.StartUpdatingLocation();
        });
    }

    private void LocationsUpdated(object? sender, CLLocationsUpdatedEventArgs? e)
    {
        var locations = e!.Locations;
        var loc = new Models.Location(locations[^1].Coordinate.Latitude, locations[^1].Coordinate.Longitude,
            (float)locations[^1].Course, locations[^1].Altitude, (float)locations[^1].CourseAccuracy!);
        Task.Run(async () =>
            await _jsRuntime.InprocessJsRuntime!.InvokeVoidAsync(
                $"MauiBundler.Plugins.{nameof(GeoLocation)}.locationChanged", loc));
    }

    [JSInvokable("stop")]
    public void Stop()
        => _iosLocationManager.StopUpdatingLocation();
    
    private async Task PublishStatusChangedEvent(string message)
        => await _jsRuntime.InprocessJsRuntime!.InvokeVoidAsync(
            $"MauiBundler.Plugins.{nameof(GeoLocation)}.statusChanged", message);
}
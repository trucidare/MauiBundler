using CoreLocation;
using MauiBundler.Abstractions.Interfaces;
using Plugin.Helper;
using Microsoft.JSInterop;

namespace Plugin.Geolocation.Services;

public class GeoLocationService : IGeoLocationService
{
    private readonly IPluginService jsRuntime = ServiceHelper.GetService<IPluginService>();

    private CLLocationManager iosLocationManager;

    public GeoLocationService()
    {
        iosLocationManager ??= new CLLocationManager()
        {
            DesiredAccuracy = CLLocation.AccuracyBest,
            DistanceFilter = CLLocationDistance.FilterNone,
            PausesLocationUpdatesAutomatically = false
        };
    }

    public void Start()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                Task.Run(async () => await PublishStatusChangedEvent("Permission for location is not granted, we can't get location updates"));
                return;
            }
            iosLocationManager.RequestAlwaysAuthorization();
            iosLocationManager.LocationsUpdated += LocationsUpdated;
            iosLocationManager.StartUpdatingLocation();
        });
    }

     private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            var locations = e.Locations;
            var loc = new Models.Location(locations[^1].Coordinate.Latitude, locations[^1].Coordinate.Longitude, (float)locations[^1].Course, locations[^1].Altitude, (float)locations[^1].CourseAccuracy);
            Task.Run(async () => await jsRuntime.InprocessJSRuntime!.InvokeVoidAsync($"MauiBundler.Plugins.{nameof(GeoLocation)}.locationChanged", loc));
        }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    private async Task PublishStatusChangedEvent(string message)
    {
        await jsRuntime.InprocessJSRuntime!.InvokeVoidAsync($"MauiBundler.Plugins.{nameof(GeoLocation)}.statusChanged", message);
    }
}
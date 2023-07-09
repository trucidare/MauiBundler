using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using MauiBundler.Abstractions.Interfaces;
using Microsoft.JSInterop;
using Plugin.Helper;

namespace Plugin.Geolocation.Services;

public class GeoLocationService : Java.Lang.Object, IGeoLocationService, ILocationListener
{
    private readonly IPluginService jsRuntime = ServiceHelper.GetService<IPluginService>();

    private readonly LocationManager locationManager;

    public GeoLocationService()
    {
        locationManager ??= (LocationManager)MauiApplication.Current.GetSystemService(Context.LocationService)!;
    }

    [JSInvokable("start")]
    public void Start()
    {
        if (locationManager != null)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    PublishStatusChangedEvent("Permission for location is not granted, we can't get location updates").GetAwaiter().GetResult();
                    return;
                }
                if (!locationManager.IsLocationEnabled)
                {
                    PublishStatusChangedEvent("Location is not enabled, we can't get location updates").GetAwaiter().GetResult();
                    return;
                }
                if (!locationManager.IsProviderEnabled(LocationManager.GpsProvider))
                {
                    PublishStatusChangedEvent("GPS Provider is not enabled, we can't get location updates").GetAwaiter().GetResult();
                    return;
                }
                locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 800, 1, this);
            });

        }
    }

    [JSInvokable("stop")]
    public void Stop()
    {
        locationManager?.RemoveUpdates(this);
    }

    public void OnLocationChanged(Android.Locations.Location location)
    {
        if (location != null)
        {
            var loc = new Models.Location(location.Latitude, location.Longitude, location.Bearing, location.Altitude, location.Accuracy);
            Task.Run(async () => await jsRuntime.InprocessJSRuntime!.InvokeVoidAsync($"MauiBundler.Plugins.{nameof(GeoLocation)}.locationChanged", loc));
        }
    }

    public void OnProviderDisabled(string provider)
    {
        Task.Run(async () => await PublishStatusChangedEvent($"{provider} has been disabled"));
    }

    public void OnProviderEnabled(string provider)
    {
        Task.Run(async () => await PublishStatusChangedEvent($"{provider} now enabled"));
    }

    public void OnStatusChanged(string? provider, [GeneratedEnum] Availability status, Bundle? extras)
    {
        Task.Run(async () => await PublishStatusChangedEvent($"{provider} change his status and now it's {status}"));
    }

    private async Task PublishStatusChangedEvent(string message)
    {
        await jsRuntime.InprocessJSRuntime!.InvokeVoidAsync($"MauiBundler.Plugins.{nameof(GeoLocation)}.statusChanged", message);
    }
}
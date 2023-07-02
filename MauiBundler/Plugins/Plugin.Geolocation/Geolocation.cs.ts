export class GeoPosition {
    accuracy: number;
    altitude: number;
    bearing: number;
    latitude: number;
    longitude: number;
}

class GeoLocation extends PluginBase {
    #handler = [];

    start() {
        this.invokeMethodAsync("start");
    }

    stop() {
        this.invokeMethodAsync("stop");
    }

    addLocationHandler(cb) {
        this.#handler.push(cb);
    }

    locationChanged(location: GeoPosition) {
        this.#handler.forEach(
            cb => cb(location)
        );
    }

    statusChanged(message) {
        console.log(message);
    }
}

MauiBundler.Plugins.GeoLocation = new GeoLocation();

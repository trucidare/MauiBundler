var __classPrivateFieldGet = (this && this.__classPrivateFieldGet) || function (receiver, state, kind, f) {
    if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a getter");
    if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot read private member from an object whose class did not declare it");
    return kind === "m" ? f : kind === "a" ? f.call(receiver) : f ? f.value : state.get(receiver);
};
var _GeoLocation_handler;
export class GeoPosition {
}
class GeoLocation extends PluginBase {
    constructor() {
        super(...arguments);
        _GeoLocation_handler.set(this, []);
    }
    start() {
        this.invokeMethodAsync("start");
    }
    stop() {
        this.invokeMethodAsync("stop");
    }
    addLocationHandler(cb) {
        __classPrivateFieldGet(this, _GeoLocation_handler, "f").push(cb);
    }
    locationChanged(location) {
        __classPrivateFieldGet(this, _GeoLocation_handler, "f").forEach(cb => cb(location));
    }
    statusChanged(message) {
        console.log(message);
    }
}
_GeoLocation_handler = new WeakMap();
MauiBundler.Plugins.GeoLocation = new GeoLocation();

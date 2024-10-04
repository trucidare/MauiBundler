var __classPrivateFieldGet = (this && this.__classPrivateFieldGet) || function (receiver, state, kind, f) {
    if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a getter");
    if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot read private member from an object whose class did not declare it");
    return kind === "m" ? f : kind === "a" ? f.call(receiver) : f ? f.value : state.get(receiver);
};
var _Intent_handler;
class Intent extends PluginBase {
    constructor() {
        super(...arguments);
        _Intent_handler.set(this, []);
    }
    addFilter(category, action, cb) {
        if (cb)
            __classPrivateFieldGet(this, _Intent_handler, "f").push(cb);
        this.invokeMethodAsync("addIntentFilter", category, action);
        console.log("MauiBundler::JS -> AddIntentFilter");
    }
    publishIntent(category, content) {
        if (__classPrivateFieldGet(this, _Intent_handler, "f")) {
            __classPrivateFieldGet(this, _Intent_handler, "f").forEach((sub) => {
                sub(category, content);
            });
        }
    }
}
_Intent_handler = new WeakMap();
MauiBundler.Plugins.Intent = new Intent();

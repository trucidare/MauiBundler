var __classPrivateFieldGet = (this && this.__classPrivateFieldGet) || function (receiver, state, kind, f) {
    if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a getter");
    if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot read private member from an object whose class did not declare it");
    return kind === "m" ? f : kind === "a" ? f.call(receiver) : f ? f.value : state.get(receiver);
};
var _AppActions_handler;
class AppActions extends PluginBase {
    constructor() {
        super(...arguments);
        _AppActions_handler.set(this, []);
    }
    addAppAction(id, title, subtitle, icon, cb) {
        if (cb)
            __classPrivateFieldGet(this, _AppActions_handler, "f").push(cb);
        this.invokeMethodAsync("addAppAction", id, title, subtitle, icon);
        console.log("MauiBundler::JS -> AddAppActiomFilter");
    }
    appActionCalled(category, content) {
        if (__classPrivateFieldGet(this, _AppActions_handler, "f")) {
            __classPrivateFieldGet(this, _AppActions_handler, "f").forEach((sub) => {
                sub(category, content);
            });
        }
    }
}
_AppActions_handler = new WeakMap();
MauiBundler.Plugins.AppActions = new AppActions();

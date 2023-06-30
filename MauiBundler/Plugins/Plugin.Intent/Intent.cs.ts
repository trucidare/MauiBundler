class Intent extends PluginBase {
  #handler = [];

  addFilter(category, action, cb) {
    if (cb) this.#handler.push(cb);

    this.invokeMethodAsync("addIntentFilter", category, action);
    console.log("MauiBundler::JS -> AddIntentFilter");
  }

  publishIntent(category, content) {
    if (this.#handler) {
      this.#handler.forEach((sub) => {
        sub(category, content);
      });
    }
  }
}

if (MauiBundler.Plugins) MauiBundler.Plugins.Intent = new Intent();

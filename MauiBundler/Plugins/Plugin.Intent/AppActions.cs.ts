class AppActions extends PluginBase {
    #handler = [];
  
    addAppAction(id, title, subtitle, icon, cb) {
      if (cb) this.#handler.push(cb);
  
      this.invokeMethodAsync("addAppAction", id, title, subtitle, icon);
      console.log("MauiBundler::JS -> AddAppActiomFilter");
    }
  
    appActionCalled(category, content) {
      if (this.#handler) {
        this.#handler.forEach((sub) => {
          sub(category, content);
        });
      }
    }
  }
  
  MauiBundler.Plugins.AppActions = new AppActions();
  
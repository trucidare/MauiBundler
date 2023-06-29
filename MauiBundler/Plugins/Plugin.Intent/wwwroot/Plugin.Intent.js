class Intent {
    #namespace = null;
    #dotnetRef = null;
    #handler = [];
  
    initialize(namespace, dotnetRed) {
      this.#namespace = namespace;
      this.#dotnetRef = dotnetRed;
      
      console.log('MauiBundler::JS -> Initialized Intent!');
    }

    addFilter(category, action, cb) {
      if (cb)
        this.#handler.push(cb)

      this.#dotnetRef.invokeMethodAsync("addIntentFilter", category, action);
      console.log("MauiBundler::JS -> AddIntentFilter");
    }


    publishIntent = (category, content) => {
      if (this.#handler) {
          this.#handler.forEach(sub => {
              sub(category, content);
          });
      }
    }
}

if (window.MauiBundler.Plugins)
  window.MauiBundler.Plugins.Intent = new Intent();
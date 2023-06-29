class Geolocation {
    #namespace = null;
    #dotnetRef = null;
  
    initialize(namespace, dotnetRed) {
      this.#namespace = namespace;
      this.#dotnetRef = dotnetRed;
  
      console.log('MauiBundler::JS -> Initialized Geolocation!');
    }
}

if (window.MauiBundler.Plugins)
  window.MauiBundler.Plugins.Geolocation = new Geolocation();
class Filesystem {
  #namespace = null;
  #dotnetRef = null;

  initialize(namespace, dotnetRed) {
    this.#namespace = namespace;
    this.#dotnetRef = dotnetRed;

    console.log('MauiBundler::JS -> Initialized Filesystem!');
  }
}

if (window.MauiBundler.Plugins)
  window.MauiBundler.Plugins.Filesystem = new Filesystem();
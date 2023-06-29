export class BasePlugin {
    namespace = null;
    dotnetRef = null;

    initialize(namespace, dotnetRed) {
        this.namespace = namespace;
        this.dotnetRef = dotnetRed;
        
        console.log('MauiBundler::JS -> Initialized Intent!');
      }
  
}
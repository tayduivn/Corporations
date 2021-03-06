//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class BrandingEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IBrandingListener> _listenerBuffer;

    public BrandingEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IBrandingListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.Branding)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasBranding && entity.hasBrandingListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.branding;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.brandingListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnBranding(e, component.BrandPower);
            }
        }
    }
}

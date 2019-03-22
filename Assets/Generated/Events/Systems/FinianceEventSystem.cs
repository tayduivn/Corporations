//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class FinianceEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IFinianceListener> _listenerBuffer;

    public FinianceEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IFinianceListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.Finiance)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasFiniance && entity.hasFinianceListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.finiance;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.finianceListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnFiniance(e, component.price, component.marketingFinancing, component.salaries);
            }
        }
    }
}

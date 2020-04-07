//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class ShareholdersEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IShareholdersListener> _listenerBuffer;

    public ShareholdersEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IShareholdersListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.Shareholders)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasShareholders && entity.hasShareholdersListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.shareholders;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.shareholdersListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnShareholders(e, component.Shareholders);
            }
        }
    }
}
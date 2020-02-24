//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class NavigationHistoryEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<INavigationHistoryListener> _listenerBuffer;

    public NavigationHistoryEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<INavigationHistoryListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.NavigationHistory)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasNavigationHistory && entity.hasNavigationHistoryListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.navigationHistory;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.navigationHistoryListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnNavigationHistory(e, component.Queries);
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class CompanyGoalEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ICompanyGoalListener> _listenerBuffer;

    public CompanyGoalEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ICompanyGoalListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.CompanyGoal)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasCompanyGoal && entity.hasCompanyGoalListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.companyGoal;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.companyGoalListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnCompanyGoal(e, component.Goals);
            }
        }
    }
}
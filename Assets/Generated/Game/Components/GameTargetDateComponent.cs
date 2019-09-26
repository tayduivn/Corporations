//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity targetDateEntity { get { return GetGroup(GameMatcher.TargetDate).GetSingleEntity(); } }
    public TargetDateComponent targetDate { get { return targetDateEntity.targetDate; } }
    public bool hasTargetDate { get { return targetDateEntity != null; } }

    public GameEntity SetTargetDate(int newDate) {
        if (hasTargetDate) {
            throw new Entitas.EntitasException("Could not set TargetDate!\n" + this + " already has an entity with TargetDateComponent!",
                "You should check if the context already has a targetDateEntity before setting it or use context.ReplaceTargetDate().");
        }
        var entity = CreateEntity();
        entity.AddTargetDate(newDate);
        return entity;
    }

    public void ReplaceTargetDate(int newDate) {
        var entity = targetDateEntity;
        if (entity == null) {
            entity = SetTargetDate(newDate);
        } else {
            entity.ReplaceTargetDate(newDate);
        }
    }

    public void RemoveTargetDate() {
        targetDateEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TargetDateComponent targetDate { get { return (TargetDateComponent)GetComponent(GameComponentsLookup.TargetDate); } }
    public bool hasTargetDate { get { return HasComponent(GameComponentsLookup.TargetDate); } }

    public void AddTargetDate(int newDate) {
        var index = GameComponentsLookup.TargetDate;
        var component = (TargetDateComponent)CreateComponent(index, typeof(TargetDateComponent));
        component.Date = newDate;
        AddComponent(index, component);
    }

    public void ReplaceTargetDate(int newDate) {
        var index = GameComponentsLookup.TargetDate;
        var component = (TargetDateComponent)CreateComponent(index, typeof(TargetDateComponent));
        component.Date = newDate;
        ReplaceComponent(index, component);
    }

    public void RemoveTargetDate() {
        RemoveComponent(GameComponentsLookup.TargetDate);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTargetDate;

    public static Entitas.IMatcher<GameEntity> TargetDate {
        get {
            if (_matcherTargetDate == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TargetDate);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTargetDate = matcher;
            }

            return _matcherTargetDate;
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RuledByComponent ruledBy { get { return (RuledByComponent)GetComponent(GameComponentsLookup.RuledBy); } }
    public bool hasRuledBy { get { return HasComponent(GameComponentsLookup.RuledBy); } }

    public void AddRuledBy(HumanComponent newHuman, int newCompanyId) {
        var index = GameComponentsLookup.RuledBy;
        var component = (RuledByComponent)CreateComponent(index, typeof(RuledByComponent));
        component.human = newHuman;
        component.CompanyId = newCompanyId;
        AddComponent(index, component);
    }

    public void ReplaceRuledBy(HumanComponent newHuman, int newCompanyId) {
        var index = GameComponentsLookup.RuledBy;
        var component = (RuledByComponent)CreateComponent(index, typeof(RuledByComponent));
        component.human = newHuman;
        component.CompanyId = newCompanyId;
        ReplaceComponent(index, component);
    }

    public void RemoveRuledBy() {
        RemoveComponent(GameComponentsLookup.RuledBy);
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

    static Entitas.IMatcher<GameEntity> _matcherRuledBy;

    public static Entitas.IMatcher<GameEntity> RuledBy {
        get {
            if (_matcherRuledBy == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RuledBy);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRuledBy = matcher;
            }

            return _matcherRuledBy;
        }
    }
}

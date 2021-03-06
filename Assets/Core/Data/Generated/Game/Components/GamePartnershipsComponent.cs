//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PartnershipsComponent partnerships { get { return (PartnershipsComponent)GetComponent(GameComponentsLookup.Partnerships); } }
    public bool hasPartnerships { get { return HasComponent(GameComponentsLookup.Partnerships); } }

    public void AddPartnerships(System.Collections.Generic.List<int> newCompanies) {
        var index = GameComponentsLookup.Partnerships;
        var component = (PartnershipsComponent)CreateComponent(index, typeof(PartnershipsComponent));
        component.companies = newCompanies;
        AddComponent(index, component);
    }

    public void ReplacePartnerships(System.Collections.Generic.List<int> newCompanies) {
        var index = GameComponentsLookup.Partnerships;
        var component = (PartnershipsComponent)CreateComponent(index, typeof(PartnershipsComponent));
        component.companies = newCompanies;
        ReplaceComponent(index, component);
    }

    public void RemovePartnerships() {
        RemoveComponent(GameComponentsLookup.Partnerships);
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

    static Entitas.IMatcher<GameEntity> _matcherPartnerships;

    public static Entitas.IMatcher<GameEntity> Partnerships {
        get {
            if (_matcherPartnerships == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Partnerships);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPartnerships = matcher;
            }

            return _matcherPartnerships;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CompanyResourceComponent companyResource { get { return (CompanyResourceComponent)GetComponent(GameComponentsLookup.CompanyResource); } }
    public bool hasCompanyResource { get { return HasComponent(GameComponentsLookup.CompanyResource); } }

    public void AddCompanyResource(Assets.Core.TeamResource newResources) {
        var index = GameComponentsLookup.CompanyResource;
        var component = (CompanyResourceComponent)CreateComponent(index, typeof(CompanyResourceComponent));
        component.Resources = newResources;
        AddComponent(index, component);
    }

    public void ReplaceCompanyResource(Assets.Core.TeamResource newResources) {
        var index = GameComponentsLookup.CompanyResource;
        var component = (CompanyResourceComponent)CreateComponent(index, typeof(CompanyResourceComponent));
        component.Resources = newResources;
        ReplaceComponent(index, component);
    }

    public void RemoveCompanyResource() {
        RemoveComponent(GameComponentsLookup.CompanyResource);
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

    static Entitas.IMatcher<GameEntity> _matcherCompanyResource;

    public static Entitas.IMatcher<GameEntity> CompanyResource {
        get {
            if (_matcherCompanyResource == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CompanyResource);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCompanyResource = matcher;
            }

            return _matcherCompanyResource;
        }
    }
}

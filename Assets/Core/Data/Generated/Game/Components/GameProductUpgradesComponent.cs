//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ProductUpgradesComponent productUpgrades { get { return (ProductUpgradesComponent)GetComponent(GameComponentsLookup.ProductUpgrades); } }
    public bool hasProductUpgrades { get { return HasComponent(GameComponentsLookup.ProductUpgrades); } }

    public void AddProductUpgrades(System.Collections.Generic.Dictionary<ProductUpgrade, bool> newUpgrades) {
        var index = GameComponentsLookup.ProductUpgrades;
        var component = (ProductUpgradesComponent)CreateComponent(index, typeof(ProductUpgradesComponent));
        component.upgrades = newUpgrades;
        AddComponent(index, component);
    }

    public void ReplaceProductUpgrades(System.Collections.Generic.Dictionary<ProductUpgrade, bool> newUpgrades) {
        var index = GameComponentsLookup.ProductUpgrades;
        var component = (ProductUpgradesComponent)CreateComponent(index, typeof(ProductUpgradesComponent));
        component.upgrades = newUpgrades;
        ReplaceComponent(index, component);
    }

    public void RemoveProductUpgrades() {
        RemoveComponent(GameComponentsLookup.ProductUpgrades);
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

    static Entitas.IMatcher<GameEntity> _matcherProductUpgrades;

    public static Entitas.IMatcher<GameEntity> ProductUpgrades {
        get {
            if (_matcherProductUpgrades == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ProductUpgrades);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherProductUpgrades = matcher;
            }

            return _matcherProductUpgrades;
        }
    }
}

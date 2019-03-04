//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ProductComponent product { get { return (ProductComponent)GetComponent(GameComponentsLookup.Product); } }
    public bool hasProduct { get { return HasComponent(GameComponentsLookup.Product); } }

    public void AddProduct(int newId, string newName, Niche newNiche, Industry newIndustry, int newProductLevel, int newExplorationLevel, Assets.Classes.TeamResource newResources) {
        var index = GameComponentsLookup.Product;
        var component = (ProductComponent)CreateComponent(index, typeof(ProductComponent));
        component.Id = newId;
        component.Name = newName;
        component.Niche = newNiche;
        component.Industry = newIndustry;
        component.ProductLevel = newProductLevel;
        component.ExplorationLevel = newExplorationLevel;
        component.Resources = newResources;
        AddComponent(index, component);
    }

    public void ReplaceProduct(int newId, string newName, Niche newNiche, Industry newIndustry, int newProductLevel, int newExplorationLevel, Assets.Classes.TeamResource newResources) {
        var index = GameComponentsLookup.Product;
        var component = (ProductComponent)CreateComponent(index, typeof(ProductComponent));
        component.Id = newId;
        component.Name = newName;
        component.Niche = newNiche;
        component.Industry = newIndustry;
        component.ProductLevel = newProductLevel;
        component.ExplorationLevel = newExplorationLevel;
        component.Resources = newResources;
        ReplaceComponent(index, component);
    }

    public void RemoveProduct() {
        RemoveComponent(GameComponentsLookup.Product);
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

    static Entitas.IMatcher<GameEntity> _matcherProduct;

    public static Entitas.IMatcher<GameEntity> Product {
        get {
            if (_matcherProduct == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Product);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherProduct = matcher;
            }

            return _matcherProduct;
        }
    }
}

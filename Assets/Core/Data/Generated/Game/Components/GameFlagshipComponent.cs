//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly FlagshipComponent flagshipComponent = new FlagshipComponent();

    public bool isFlagship {
        get { return HasComponent(GameComponentsLookup.Flagship); }
        set {
            if (value != isFlagship) {
                var index = GameComponentsLookup.Flagship;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : flagshipComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherFlagship;

    public static Entitas.IMatcher<GameEntity> Flagship {
        get {
            if (_matcherFlagship == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Flagship);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFlagship = matcher;
            }

            return _matcherFlagship;
        }
    }
}

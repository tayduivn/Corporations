//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnyDateListenerComponent anyDateListener { get { return (AnyDateListenerComponent)GetComponent(GameComponentsLookup.AnyDateListener); } }
    public bool hasAnyDateListener { get { return HasComponent(GameComponentsLookup.AnyDateListener); } }

    public void AddAnyDateListener(System.Collections.Generic.List<IAnyDateListener> newValue) {
        var index = GameComponentsLookup.AnyDateListener;
        var component = (AnyDateListenerComponent)CreateComponent(index, typeof(AnyDateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyDateListener(System.Collections.Generic.List<IAnyDateListener> newValue) {
        var index = GameComponentsLookup.AnyDateListener;
        var component = (AnyDateListenerComponent)CreateComponent(index, typeof(AnyDateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyDateListener() {
        RemoveComponent(GameComponentsLookup.AnyDateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherAnyDateListener;

    public static Entitas.IMatcher<GameEntity> AnyDateListener {
        get {
            if (_matcherAnyDateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnyDateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnyDateListener = matcher;
            }

            return _matcherAnyDateListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddAnyDateListener(IAnyDateListener value) {
        var listeners = hasAnyDateListener
            ? anyDateListener.value
            : new System.Collections.Generic.List<IAnyDateListener>();
        listeners.Add(value);
        ReplaceAnyDateListener(listeners);
    }

    public void RemoveAnyDateListener(IAnyDateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyDateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyDateListener();
        } else {
            ReplaceAnyDateListener(listeners);
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ReleaseListenerComponent releaseListener { get { return (ReleaseListenerComponent)GetComponent(GameComponentsLookup.ReleaseListener); } }
    public bool hasReleaseListener { get { return HasComponent(GameComponentsLookup.ReleaseListener); } }

    public void AddReleaseListener(System.Collections.Generic.List<IReleaseListener> newValue) {
        var index = GameComponentsLookup.ReleaseListener;
        var component = (ReleaseListenerComponent)CreateComponent(index, typeof(ReleaseListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceReleaseListener(System.Collections.Generic.List<IReleaseListener> newValue) {
        var index = GameComponentsLookup.ReleaseListener;
        var component = (ReleaseListenerComponent)CreateComponent(index, typeof(ReleaseListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveReleaseListener() {
        RemoveComponent(GameComponentsLookup.ReleaseListener);
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

    static Entitas.IMatcher<GameEntity> _matcherReleaseListener;

    public static Entitas.IMatcher<GameEntity> ReleaseListener {
        get {
            if (_matcherReleaseListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReleaseListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReleaseListener = matcher;
            }

            return _matcherReleaseListener;
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

    public void AddReleaseListener(IReleaseListener value) {
        var listeners = hasReleaseListener
            ? releaseListener.value
            : new System.Collections.Generic.List<IReleaseListener>();
        listeners.Add(value);
        ReplaceReleaseListener(listeners);
    }

    public void RemoveReleaseListener(IReleaseListener value, bool removeComponentWhenEmpty = true) {
        var listeners = releaseListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveReleaseListener();
        } else {
            ReplaceReleaseListener(listeners);
        }
    }
}
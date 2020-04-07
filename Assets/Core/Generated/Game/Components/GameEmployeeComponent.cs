//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public EmployeeComponent employee { get { return (EmployeeComponent)GetComponent(GameComponentsLookup.Employee); } }
    public bool hasEmployee { get { return HasComponent(GameComponentsLookup.Employee); } }

    public void AddEmployee(System.Collections.Generic.Dictionary<int, WorkerRole> newManagers) {
        var index = GameComponentsLookup.Employee;
        var component = (EmployeeComponent)CreateComponent(index, typeof(EmployeeComponent));
        component.Managers = newManagers;
        AddComponent(index, component);
    }

    public void ReplaceEmployee(System.Collections.Generic.Dictionary<int, WorkerRole> newManagers) {
        var index = GameComponentsLookup.Employee;
        var component = (EmployeeComponent)CreateComponent(index, typeof(EmployeeComponent));
        component.Managers = newManagers;
        ReplaceComponent(index, component);
    }

    public void RemoveEmployee() {
        RemoveComponent(GameComponentsLookup.Employee);
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

    static Entitas.IMatcher<GameEntity> _matcherEmployee;

    public static Entitas.IMatcher<GameEntity> Employee {
        get {
            if (_matcherEmployee == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Employee);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEmployee = matcher;
            }

            return _matcherEmployee;
        }
    }
}
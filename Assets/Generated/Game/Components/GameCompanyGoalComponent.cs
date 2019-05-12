//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CompanyGoalComponent companyGoal { get { return (CompanyGoalComponent)GetComponent(GameComponentsLookup.CompanyGoal); } }
    public bool hasCompanyGoal { get { return HasComponent(GameComponentsLookup.CompanyGoal); } }

    public void AddCompanyGoal(InvestorGoal newInvestorGoal, int newExpires, long newMeasurableGoal) {
        var index = GameComponentsLookup.CompanyGoal;
        var component = (CompanyGoalComponent)CreateComponent(index, typeof(CompanyGoalComponent));
        component.InvestorGoal = newInvestorGoal;
        component.Expires = newExpires;
        component.MeasurableGoal = newMeasurableGoal;
        AddComponent(index, component);
    }

    public void ReplaceCompanyGoal(InvestorGoal newInvestorGoal, int newExpires, long newMeasurableGoal) {
        var index = GameComponentsLookup.CompanyGoal;
        var component = (CompanyGoalComponent)CreateComponent(index, typeof(CompanyGoalComponent));
        component.InvestorGoal = newInvestorGoal;
        component.Expires = newExpires;
        component.MeasurableGoal = newMeasurableGoal;
        ReplaceComponent(index, component);
    }

    public void RemoveCompanyGoal() {
        RemoveComponent(GameComponentsLookup.CompanyGoal);
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

    static Entitas.IMatcher<GameEntity> _matcherCompanyGoal;

    public static Entitas.IMatcher<GameEntity> CompanyGoal {
        get {
            if (_matcherCompanyGoal == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CompanyGoal);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCompanyGoal = matcher;
            }

            return _matcherCompanyGoal;
        }
    }
}

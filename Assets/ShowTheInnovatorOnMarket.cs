﻿using Assets.Utils;
using System.Linq;

public class ShowTheInnovatorOnMarket : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var players = Markets.GetProductsOnMarket(GameContext, SelectedNiche);

        var productCompany = players.FirstOrDefault(p => p.isTechnologyLeader);

        if (productCompany == null)
            return "Noone has the decisive advantage in concept";

        return $"{productCompany.company.Name} ({ProductUtils.GetProductLevel(productCompany)}LVL) \nThis gives them +1 Brand each month";
    }
}

﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLoyaltyChange : ParameterView
{
    public override string RenderValue()
    {
        var text = "";

        var human = SelectedHuman;

        var isEmployed = human.worker.companyId >= 0;

        if (!isEmployed)
            return "";

        var company = Companies.Get(Q, human.worker.companyId);

        var culture = Companies.GetActualCorporateCulture(company, Q);

        var changeBonus = Teams.GetLoyaltyChangeBonus(human, culture, company, Q);
        var change = changeBonus.Sum();

        text += "\n\n";

        bool worksInMyCompany = Humans.IsWorksInCompany(human, MyCompany.company.Id) || Humans.IsWorksInCompany(human, Flagship.company.Id);

        if (worksInMyCompany)
        {
            // TODO copypasted in HumanPreview.cs
            text += Visuals.DescribeValueWithText(change,
                $"Enjoys this company!\nWeekly loyalty change: +{change}",
                $"Doesn't like this company!\nWeekly loyalty change: {change}",
                "Is satisfied by this company"
                );

            text += "\n";

            text += changeBonus.ToString();
        }

        return text;
    }
}

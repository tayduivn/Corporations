﻿using Assets.Core;
using System;
using System.Linq;

public class LinkToUnhappyTeam : ButtonController
{
    public override void Execute()
    {
        var companies = Companies.GetDaughterUnhappyCompanies(Q, MyCompany);

        var hint = $"You have {companies.Length} exhausted teams.\n\n" + String.Join("\n", companies.Select(p => p.company.Name));
        GetComponent<Hint>().SetHint(hint);

        var targetMenu = ScreenMode.TeamScreen;



        var companyId = SelectedCompany.company.Id;

        if (companies.Length == 0)
            return;

        var firstId = companies.First().company.Id;


        if (CurrentScreen != targetMenu)
            companyId = firstId;
        else
        {
            var ind = Array.FindIndex(companies, m => m.company.Id == companyId);

            if (ind == -1 || ind == companies.Length - 1)
                companyId = firstId;
            else
                companyId = companies[ind + 1].company.Id;
        }



        Navigate(targetMenu, C.MENU_SELECTED_COMPANY, companyId);
    }
}

﻿using Assets.Utils;
using UnityEngine.UI;

public class CheckIPORequirements : View
{
    public Button IPOButton;
    public Hint Hint;


    public override void ViewRender()
    {
        base.ViewRender();


        int companyId = SelectedCompany.company.Id;

        Hint.SetHint($"Requirements" +
            Visuals.Colorize($"\nCompany Cost more than ${Format.Minify(Constants.IPO_REQUIREMENTS_COMPANY_COST)}", CompanyUtils.IsMeetsIPOCompanyCostRequirement(GameContext, companyId))  +
            Visuals.Colorize($"\nMore than 3 shareholders", CompanyUtils.IsMeetsIPOShareholderRequirement(GameContext, companyId)) + 
            Visuals.Colorize($"\nProfit bigger than ${Format.Minify(Constants.IPO_REQUIREMENTS_COMPANY_PROFIT)}", CompanyUtils.IsMeetsIPOProfitRequirement(GameContext, companyId))
            );

        IPOButton.interactable = CompanyUtils.IsCanGoPublic(GameContext, SelectedCompany.company.Id);
    }
}

﻿using Assets.Utils;

public class ShowMyGroupLink : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return true;
        return CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id).Length > 0;
    }
}


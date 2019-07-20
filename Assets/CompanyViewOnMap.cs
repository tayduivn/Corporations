﻿using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMap : View
{
    public Text Name;
    public Hint CompanyHint;
    public LinkToProjectView LinkToProjectView;

    public Image Image;

    GameEntity company;

    public void SetEntity(GameEntity c)
    {
        company = c;
        bool hasControl = CompanyUtils.GetControlInCompany(MyCompany, c, GameContext) > 0;

        Name.text = c.company.Name.Substring(0, 1);
        Name.color = Visuals.Color(hasControl ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_NEUTRAL);

        LinkToProjectView.CompanyId = c.company.Id;

        CompanyHint.SetHint(GetCompanyHint(hasControl));
    }

    string GetCompanyHint(bool hasControl)
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\n\nWe control this company", VisualConstants.COLOR_CONTROL));

        var position = NicheUtils.GetPositionOnMarket(GameContext, company);
        var nicheName = EnumUtils.GetFormattedNicheName(company.product.Niche);
        hint.AppendFormat("\n\n#{0} on market of {1}", position + 1, nicheName);

        return hint.ToString();
    }
}
﻿using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RenderCompanyCompetitors : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        // previous prefab: StrategicPartnershipView

        var company = entity as GameEntity;
        var companyId = company.company.Id;

        //t.GetComponent<LinkToProjectView>().CompanyId = companyId;
        //t.GetComponent<RenderPartnerName>().SetCompanyId(companyId);

        t.gameObject.AddComponent<Button>();
        t.gameObject.AddComponent<LinkToProjectView>().CompanyId = companyId;

        string text = company.company.Name;

        if (Companies.IsDirectlyRelatedToPlayer(Q, company))
            text = Visuals.Colorize(text, Colors.COLOR_GOLD);

        var sameMarkets = new List<NicheType>();

        var m1 = SelectedCompany.companyFocus.Niches;
        var m2 = company.companyFocus.Niches;

        foreach (var m in m1)
        {
            if (m2.Contains(m))
                sameMarkets.Add(m);
        }

        if (company.hasProduct)
        {
            var innovativeness = Products.GetInnovationChance(company, Q);

            var level = Products.GetProductLevel(company);

            text += $" ({level}LVL)\n(Innovativeness: {innovativeness}%)";
        }
        else
        {
            var cost = Economy.CostOf(company, Q);

            text += $" (Cost: {Format.Money(cost)})";
            text += "\nCommon markets: " + String.Join(", ", sameMarkets.Select(Enums.GetFormattedNicheName));
        }



        t.gameObject.GetComponent<MockText>().SetEntity(text);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOf(SelectedCompany, Q, true);

        SetItems(competitors);
    }
}
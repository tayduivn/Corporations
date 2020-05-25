﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderProductStatsInCompanyView : View
{
    public Text Clients;

    public GameObject MarketShare;
    public GameObject MarketShareLabel;

    public Text ProductLevel;
    public GameObject ProductLevelLabel;

    public Text Brand;
    public GameObject BrandIcon;

    public Text Workers;
    public GameObject WorkersLabel;


    public void Render(GameEntity company)
    {
        Clients.text = Format.Minify(Marketing.GetClients(company));

        // market share
        bool isPlayerFlagship = company.company.Id == Flagship.company.Id;
        bool needToShowMarketShare = company.isRelease && isPlayerFlagship;

        Draw(MarketShare, needToShowMarketShare);
        Draw(MarketShareLabel, false);

        // product level
        var levelStatus = Products.GetConceptStatus(company, Q);
        var statusColor = Colors.COLOR_WHITE;

        if (levelStatus == ConceptStatus.Leader)
            statusColor = Colors.COLOR_BEST;

        if (levelStatus == ConceptStatus.Outdated)
            statusColor = Colors.COLOR_NEGATIVE;

        var outOf = "";
        if (!company.isRelease)
        {
            var market = Markets.Get(Q, company);
            outOf = $"/{Products.GetMarketDemand(market)}";
        }

        ProductLevel.text = Visuals.Colorize(Products.GetProductLevel(company) + outOf + "LVL", statusColor);

        // brand
        Brand.text = (int)company.branding.BrandPower + "";
        Draw(BrandIcon, company.isRelease);
        Draw(Brand, company.isRelease);

        // workers
    }
}
﻿using Assets.Utils;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMap : View
{
    public Text Name;
    public Hint CompanyHint;
    public Text Concept;
    public LinkToProjectView LinkToProjectView;

    public Image Image;
    public Image DarkImage;
    public Image RelevancyImage;

    public Text Profitability;

    public RenderDumpingHint Dumping;

    public RenderConceptProgress ConceptProgress;

    bool EnableDarkTheme;

    GameEntity company;

    public void SetEntity(GameEntity c, bool darkImage)
    {
        company = c;
        EnableDarkTheme = darkImage;

        bool hasControl = Companies.GetControlInCompany(MyCompany, c, GameContext) > 0;

        Name.text = c.company.Name; // .Substring(0, 1);
        Name.color = Visuals.GetColorFromString(hasControl ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_NEUTRAL);

        LinkToProjectView.CompanyId = c.company.Id;

        var isRelatedToPlayer = Companies.IsCompanyRelatedToPlayer(GameContext, c);
        ConceptProgress.SetCompanyId(c.company.Id);
        ConceptProgress.gameObject.SetActive(isRelatedToPlayer);

        CompanyHint.SetHint(GetCompanyHint(hasControl));

        Concept.text = ProductUtils.GetProductLevel(c) + "LVL";

        SetEmblemColor();

        Dumping.SetCompanyId(c.company.Id);

        if (Profitability != null)
        {
            var profit = EconomyUtils.GetProfit(GameContext, company.company.Id);

            Profitability.text = Visuals.DescribeValueWithText(profit, "$", "$", "");
            Profitability.GetComponent<Hint>().SetHint(
                profit > 0 ?
                Visuals.Positive($"This company is profitable!\nProfit: +{Format.Money(profit)}") :
                Visuals.Negative($"This company loses {Format.Money(-profit)} each month!")
                );
        }
    }

    Color GetMarketRelevanceColor()
    {
        var concept = "";
        switch (ProductUtils.GetConceptStatus(company, GameContext))
        {
            case ConceptStatus.Leader: concept = VisualConstants.COLOR_POSITIVE; break;
            case ConceptStatus.Outdated: concept = VisualConstants.COLOR_NEGATIVE; break;
            case ConceptStatus.Relevant: concept = VisualConstants.COLOR_NEUTRAL; break;
        }

        return Visuals.GetColorFromString(concept);
    }

    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);

        var col = DarkImage.color;
        var a = EnableDarkTheme ? 219f : 126f;

        DarkImage.color = new Color(col.r, col.g, col.b, a / 255f);
        //DarkImage.gameObject.SetActive(DisableDarkTheme);

        if (RelevancyImage != null)
            RelevancyImage.color = GetMarketRelevanceColor();
    }

    string GetCompanyHint(bool hasControl)
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        var position = Markets.GetPositionOnMarket(GameContext, company);

        // quality description
        var conceptStatus = ProductUtils.GetConceptStatus(company, GameContext);

        var concept = "???";

        switch (conceptStatus)
        {
            case ConceptStatus.Leader:      concept = Visuals.Positive("Sets Trends!"); break;
            case ConceptStatus.Outdated:    concept = Visuals.Negative("Outdated"); break;
            case ConceptStatus.Relevant:    concept = Visuals.Neutral("Relevant"); break;
        }

        //
        var level = ProductUtils.GetProductLevel(company);
        hint.AppendLine($"\nConcept: {level} ({concept})");

        var clients = MarketingUtils.GetClients(company);
        hint.AppendLine($"Clients: {clients} (#{position + 1})");

        var posTextual = Markets.GetCompanyPositioning(company, GameContext);
        hint.AppendLine($"\nPositioning: {posTextual}");

        ////var expertise = CompanyUtils.GetCompanyExpertise(company);
        //var expertise = company.expertise.ExpertiseLevel + " LVL";
        //hint.AppendLine($"\nExpertise: {expertise}");

        var brand = (int)company.branding.BrandPower;
        hint.AppendLine($"Brand: {brand}");

        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\nWe control this company", VisualConstants.COLOR_CONTROL));

        return hint.ToString();
    }
}

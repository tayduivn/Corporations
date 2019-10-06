﻿using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void AddBrandPower(GameEntity company, float power)
        {
            var brandPower = Mathf.Clamp(company.branding.BrandPower + power, 0, 100);

            company.ReplaceBranding(brandPower);
        }

        public static float GetMarketShareBasedBrandDecay(GameEntity product, GameContext gameContext)
        {
            var marketShare = (float)CompanyUtils.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext);
            var brand = product.branding.BrandPower;

            var change = (marketShare - brand) / 10;

            return change;
        }

        public static BonusContainer GetMonthlyBrandPowerChange(GameEntity product, GameContext gameContext)
        {
            bool isPayingForMarketing = TeamUtils.IsUpgradePicked(product, TeamUpgrade.Release);
            bool isPayingForAggressiveMarketing = TeamUtils.IsUpgradePicked(product, TeamUpgrade.Multiplatform);

            //Debug.Log("RecalculateBrandPowers: " + product.company.Name + " isPayingForMarketing=" + isPayingForMarketing);


            var conceptStatus = ProductUtils.GetConceptStatus(product, gameContext);
            var isOutOfMarket = conceptStatus == ConceptStatus.Outdated;
            var isInnovator = conceptStatus == ConceptStatus.Leader;

            var decay = GetMarketShareBasedBrandDecay(product, gameContext);

            var percent = 4;
            var baseDecay = -product.branding.BrandPower * percent / 100;

            var BrandingChangeBonus = new BonusContainer("Brand power change")
                //.Append("Base", -1)
                .AppendAndHideIfZero(percent + "% Decay", (int)baseDecay)
                //.Append("Due to Market share", (int)decay)
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -1 : 0)
                .AppendAndHideIfZero("Is Innovator", isInnovator ? 2 : 0)
                .AppendAndHideIfZero("Is Paying For Marketing", isPayingForMarketing ? 1 : 0)
                .AppendAndHideIfZero("Aggressive Marketing", isPayingForAggressiveMarketing ? 3 : 0);
            //.AppendAndHideIfZero("Is Innovator + Aggressive Marketing", isInnovator && isPayingForAggressiveMarketing ? 6 : 0);

            return BrandingChangeBonus;
        }
    }
}

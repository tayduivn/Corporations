﻿using Assets.Core;

public class BrandingToggleButton2 : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Branding campaign II";
    public override string GetBenefits()
    {
        var gain = C.BRAND_CAMPAIGN_BRAND_POWER_GAIN;

        return Visuals.Positive($"+{gain} Brand");
    }

    public override ProductUpgrade upgrade => ProductUpgrade.BrandCampaign2;
}


﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductUpgradeButtons : View
{
    public GameObject TargetingCampaignCheckbox;
    public GameObject TargetingCampaignCheckbox2;
    public GameObject TargetingCampaignCheckbox3;

    public GameObject SupportCheckbox;
    public GameObject SupportCheckbox2;
    public GameObject SupportCheckbox3;

    public GameObject QA;
    public GameObject QA2;
    public GameObject QA3;

    public GameObject BrandingCampaignCheckbox;
    public GameObject BrandingCampaignCheckbox2;
    public GameObject BrandingCampaignCheckbox3;

    public GameObject WebCheckbox;
    public GameObject DesktopCheckbox;
    public GameObject MobileIOSCheckbox;
    public GameObject MobileAndroidCheckbox;

    public GameObject TestCampaignCheckbox;

    public GameObject[] HiringManagers;

    public ReleaseApp ReleaseApp;

    public GameObject RaiseInvestments;

    bool CanEnable(GameEntity company, ProductUpgrade upgrade)
    {
        return Products.CanEnable(company, Q, upgrade);
    }

    void Render(GameEntity company)
    {
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        // prerelease stuff
        // ---------------------
        Draw(ReleaseApp, Companies.IsReleaseableApp(company, Q));
        Draw(TestCampaignCheckbox, !company.isRelease);

        // goal defined stuff
        // ----------------------
        Draw(SupportCheckbox,            CanEnable(company, ProductUpgrade.Support));
        Draw(SupportCheckbox2,           CanEnable(company, ProductUpgrade.Support2));
        Draw(SupportCheckbox3,           CanEnable(company, ProductUpgrade.Support3));

        Draw(QA,                         CanEnable(company, ProductUpgrade.QA));
        Draw(QA2,                        CanEnable(company, ProductUpgrade.QA2));
        Draw(QA3,                        CanEnable(company, ProductUpgrade.QA3));

        // release stuff
        // -------------
        Draw(WebCheckbox,                CanEnable(company, ProductUpgrade.PlatformWeb));
        Draw(MobileIOSCheckbox,          CanEnable(company, ProductUpgrade.PlatformMobileIOS));
        Draw(MobileAndroidCheckbox,      CanEnable(company, ProductUpgrade.PlatformMobileAndroid));
        Draw(DesktopCheckbox,            CanEnable(company, ProductUpgrade.PlatformDesktop));

        Draw(TargetingCampaignCheckbox,  CanEnable(company, ProductUpgrade.TargetingCampaign));
        Draw(TargetingCampaignCheckbox2, CanEnable(company, ProductUpgrade.TargetingCampaign2));
        Draw(TargetingCampaignCheckbox3, CanEnable(company, ProductUpgrade.TargetingCampaign3));

        Draw(BrandingCampaignCheckbox,   CanEnable(company, ProductUpgrade.BrandCampaign));
        Draw(BrandingCampaignCheckbox2,  CanEnable(company, ProductUpgrade.BrandCampaign2));
        Draw(BrandingCampaignCheckbox3,  CanEnable(company, ProductUpgrade.BrandCampaign3));


        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);
        var playerCanExploreAdvancedTabs = hasReleasedProducts;
        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);

        //var canRaiseInvestments = !isRoundActive ;
        var canRaiseInvestments = playerCanExploreAdvancedTabs || bankruptcyLooming;
        Draw(RaiseInvestments, canRaiseInvestments);

        foreach (var manager in HiringManagers)
        {
            var role = manager.GetComponent<HireManagerByRole>().WorkerRole;

            Draw(manager, CanHireManager(role, company));
        }
    }

    bool CanHireManager(WorkerRole role, GameEntity company)
    {
        return company.isRelease && Teams.HasFreePlaceForWorker(company, role);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var c = GetComponent<SpecifyCompany>();
        if (c != null)
        {
            Render(Companies.Get(Q, c.CompanyId));
            return;
        }

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        Render(flagship);
    }
}

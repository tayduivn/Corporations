﻿using Assets;
using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlagshipInterruptsView : View
{
    public Image NeedsServersImage;
    public Text ServerLoad;

    public Image NeedsSupportImage;
    public Image NeedsManagersImage;

    public Image DDOSImage;
    public ProgressBar DDOSProgress;

    //
    public Image HasDisloyalManagersImage;
    public Image AcquisitionOffer;

    public Image AudienceMapLink;
    public Text MarketShare;

    // messages
    public Image GoodMessages;
    public Text GoodMessagesAmount;

    public Image ExpiringMessages;
    public Text ExpiringMessagesAmount;

    public Image BadMessages;
    public Text BadMessagesAmount;

    int previousCounter = 0;
    int problemCounter = 0;

    public void FightServerAttack()
    {
        Flagship.serverAttack.CurrentResistance -= 2;

        if (Flagship.serverAttack.CurrentResistance <= 0)
        {
            Flagship.RemoveServerAttack();
        }

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        var load = Products.GetServerLoad(product) * 100;
        var cap = Products.GetServerCapacity(product);

        if (CurrentIntDate > 0 && CurrentIntDate % 91 == 0 && Random.Range(0, 2) < 1 && !product.hasServerAttack)
        {
            //// ddos stops when you have normal load
            //// or resistance (progressbar) < 0

            ////var load = Products.GetServerLoad(Flagship);
            //var resistance = Random.Range(10, 25);
            //var strength = Random.Range(2, 20);

            //product.AddServerAttack(cap * strength, resistance, resistance); // load x2... x20 (in pupils), resistance how easy is it to kill attack (clicks per second 1...5),
        }


        bool needsMoreServers = Products.IsNeedsMoreServers(product);
        bool needsMoreSupport = Products.IsNeedsMoreMarketingSupport(product);
        bool underAttack = product.hasServerAttack;

        if (underAttack)
        {
            product.serverAttack.CurrentResistance = Mathf.Clamp(product.serverAttack.CurrentResistance + 1, 1, product.serverAttack.Resistance);

            var res = product.serverAttack.CurrentResistance;
            var max = product.serverAttack.Resistance;

            DDOSProgress.SetValue(max - res, max);
        }
        // 
        bool hasAcquisitionOffers = Companies.GetAcquisitionOffersToPlayer(Q).Count() > 0;

        bool didFirstTasks = product.features.Upgrades.Count > 0 && Marketing.GetUsers(product) > 1000;
        bool CanManageServers = didFirstTasks || needsMoreServers;

        problemCounter = 0;

        var needsAudienceMap = Companies.IsReleaseableApp(product);

        SpecialDraw(AudienceMapLink, needsAudienceMap, false);
        AudienceMapLink.GetComponent<Blinker>().enabled = Companies.IsReleaseableApp(product);

        SpecialDraw(NeedsManagersImage, false);
        SpecialDraw(NeedsServersImage, CanManageServers, needsMoreServers);
        SpecialDraw(NeedsSupportImage, false);
        SpecialDraw(DDOSImage, underAttack);

        NeedsSupportImage.color = Visuals.GetColorFromString(needsMoreSupport ? Colors.COLOR_NEGATIVE : Colors.COLOR_NEUTRAL);
        NeedsSupportImage.GetComponent<Blinker>().enabled = needsMoreSupport;

        NeedsServersImage.color = Visuals.GetColorFromString(needsMoreServers ? Colors.COLOR_NEGATIVE : Colors.COLOR_NEUTRAL);
        NeedsServersImage.GetComponent<Blinker>().enabled = needsMoreServers;

        MarketShare.text = (Companies.GetMarketShareOfCompanyMultipliedByHundred(product, Q) * 1f).ToString("0.0") + "%";




        var perc = cap != 0 ? load / cap : 100;
        ServerLoad.text = perc + "%";
        ServerLoad.color = Visuals.GetGradientColor(0, 100, perc, true);

        SpecialDraw(HasDisloyalManagersImage, false);

        var messagesCount = NotificationUtils.GetNotifications(Q).Count;
        ExpiringMessagesAmount.text = messagesCount + "";
        SpecialDraw(GoodMessages, false);
        // THIS
        SpecialDraw(ExpiringMessages, false, false); // messagesCount > 0
        SpecialDraw(BadMessages, false);

        // hasAcquisitionOffers
        SpecialDraw(AcquisitionOffer, false);

        // play interrupt sound
        if (problemCounter > previousCounter)
        {
            SoundManager.Play(Sound.Notification);
        }

        previousCounter = problemCounter;
    }

    void SpecialDraw(Image obj, bool draw, bool contributeToProblemCounter = true)
    {
        Draw(obj, draw);

        if (draw && contributeToProblemCounter)
            problemCounter++;
    }
}

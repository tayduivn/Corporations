﻿using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;

public class NotificationRendererTrendsChange : NotificationRenderer<NotificationMessageTrendsChange>
{
    public override string GetTitle(NotificationMessageTrendsChange message)
    {
        var nicheType = message.nicheType;
        var nicheName = EnumUtils.GetFormattedNicheName(nicheType);

        var niche = NicheUtils.GetNiche(GameContext, nicheType);

        return GetShortTitle(NicheUtils.GetMarketState(niche), nicheName);
    }

    public override string GetDescription(NotificationMessageTrendsChange message)
    {
        return RenderTrendChageText(message);
    }

    public override void SetLink(NotificationMessageTrendsChange message, GameObject LinkToEvent)
    {
        var nicheType = message.nicheType;

        LinkToEvent.AddComponent<LinkToNiche>().SetNiche(nicheType);
    }

    string RenderTrendChageText(NotificationMessageTrendsChange notification)
    {
        var nicheType = notification.nicheType;

        var niche = NicheUtils.GetNiche(GameContext, nicheType);
        var phase = NicheUtils.GetMarketState(niche);

        var nicheName = EnumUtils.GetFormattedNicheName(nicheType);


        var description = "";

        switch (phase)
        {
            case NicheLifecyclePhase.Death:
                description = "People don't need them anymore and they will stop using the product. You'd better search new opportunities";
                break;

            case NicheLifecyclePhase.Decay:
                description = $"New users don't arrive anymore and we need to keep existing ones as long as possible";
                break;

            case NicheLifecyclePhase.Innovation:
                description = $"Maybe it is the next big thing?";
                break;

            case NicheLifecyclePhase.MassGrowth:
                description = $"They are well known even by those, who are not fancy to technologies";
                break;

            case NicheLifecyclePhase.Trending:
                description = $"We need to be quick if we want to make benefit from them";
                break;
        }

        return description;
    }

    static string GetShortTitle(NicheLifecyclePhase phase, string nicheName)
    {
        var description = "";

        switch (phase)
        {
            case NicheLifecyclePhase.Death:
                description = $"{nicheName} are DYING.";
                break;

            case NicheLifecyclePhase.Decay:
                description = $"{nicheName} are in DECAY.";
                break;

            case NicheLifecyclePhase.Innovation:
                description = $"{nicheName} - future or just a moment?";
                break;

            case NicheLifecyclePhase.MassGrowth:
                description = $"{nicheName} are EVERYWHERE.";
                break;

            case NicheLifecyclePhase.Trending:
                description = $"{nicheName} are TRENDING.";
                break;
        }

        return $"TRENDS change: {description}";
    }

}
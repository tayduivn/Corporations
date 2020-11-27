﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenTeamTaskView : ParameterView
{
    TeamTask TeamTask;
    public override string RenderValue()
    {
        var task = TeamTask;

        var cost = Economy.GetTeamTaskCost(Flagship, task);
        var costs = Format.Money(cost) + " / week";

        if (task is TeamTaskFeatureUpgrade)
        {
            var f = task as TeamTaskFeatureUpgrade;
            
            return $"Upgrading {f.NewProductFeature.Name} feature";
        }

        if (task is TeamTaskChannelActivity)
        {
            var f = task as TeamTaskChannelActivity;

            var channel = Markets.GetMarketingChannel(Q, f.ChannelId);
            var gain = Marketing.GetChannelClientGain(Flagship, channel);

            return $"Marketing in Channel {channel.marketingChannel.ChannelInfo.ID} (+{Format.Minify(gain)} users / week). Costs {costs}";
        }

        if (task.IsHighloadTask)
        {
            var f = task as TeamTaskSupportFeature;
            
            return $"{task.GetPrettyName()} can maintain {Format.Minify(f.SupportFeature.SupportBonus.Max)} users. Costs {costs}";
        }

        return "???";
    }

    public void SetTask(TeamTask teamTask)
    {
        TeamTask = teamTask;

        ViewRender();
    }
}

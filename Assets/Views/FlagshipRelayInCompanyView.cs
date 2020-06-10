﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipRelayInCompanyView : View
{
    // tabs
    public GameObject DevelopmentTab;
    public GameObject WorkerInteractions;

    // selected worker
    bool roleWasSelected = false;
    WorkerRole SelectedWorkerRole;



    // buttons

    private void OnEnable()
    {
        ChooseDevTab();
    }

    public void ChooseWorkerInteractions()
    {
        Draw(WorkerInteractions, true);
        Draw(DevelopmentTab, false);
    }

    public void ChooseDevTab()
    {
        Draw(WorkerInteractions, false);
        Draw(DevelopmentTab, true);
    }




    public bool IsRoleChosen(WorkerRole workerRole)
    {
        return roleWasSelected && SelectedWorkerRole == workerRole;
    }

    public void ToggleRole(WorkerRole role)
    {
        if (role == SelectedWorkerRole)
        {
            // toggling role
            roleWasSelected = !roleWasSelected;
        }
        else
        {
            // click on different role
            roleWasSelected = true;
            SelectedWorkerRole = role;

            // TODO unnecessary?
            //var up = CompanyUpgrades.GetComponent<ProductUpgradeButtons>();
            //up.WorkerRole = role;
            //up.ViewRender();
        }

        // enabled
        if (roleWasSelected)
        {
            ChooseWorkerInteractions();
        }
        else
        {
            ChooseDevTab();
        }

        MarkGameEventsAsSeen(role);

        //EnlargeOnDemand.StartAnimation();
    }


    void ClearEvents(GameEntity eventContainer, List<GameEventType> removableEvents)
    {
        var events = eventContainer.gameEventContainer.Events;

        events.RemoveAll(e => removableEvents.Contains(e.eventType));
        eventContainer.ReplaceGameEventContainer(events);
    }

    void MarkGameEventsAsSeen(WorkerRole role)
    {
        var marketingEvents = new List<GameEventType> { GameEventType.NewMarketingChannel };

        var events = NotificationUtils.GetGameEventContainerEntity(Q);

        if (role == WorkerRole.MarketingLead)
        {
            ClearEvents(events, marketingEvents);
        }
    }
}
﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerOffersListView : ListView
{
    public GameObject OffersTitle;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<JobOfferView>().SetEntity((ExpiringJobOffer)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();
        var human = SelectedHuman;

        bool hasCompetingOffers = Humans.HasCompetingOffers(human);
        Draw(OffersTitle, hasCompetingOffers);

        SetItems(hasCompetingOffers ? SelectedHuman.workerOffers.Offers : new List<ExpiringJobOffer>());
        //SetItems(SelectedHuman.workerOffers.Offers.Where(o => o.CompanyId != Flagship.company.Id));
    }
}

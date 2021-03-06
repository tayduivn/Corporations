﻿using Assets.Core;
using System.Linq;
using UnityEngine;

public class FillAIAcquisitionProposals : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var offer = (entity as GameEntity).acquisitionOffer;

        t.GetComponent<SellingOfferView>().SetEntity(offer.CompanyId, offer.BuyerId);
    }

    void Render()
    {
        var proposals = Companies.GetAcquisitionOffersToPlayer(Q)
            .OrderBy(OrderByMarketStage)
            .ToArray();

        SetItems(proposals);
    }

    int OrderByMarketStage (GameEntity a)
    {
        var c = Companies.Get(Q, a.acquisitionOffer.CompanyId);

        if (!c.hasProduct)
            return -10;

        var niche = c.product.Niche;

        var rating = Markets.GetMarketRating(Q, niche);

        return rating;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}

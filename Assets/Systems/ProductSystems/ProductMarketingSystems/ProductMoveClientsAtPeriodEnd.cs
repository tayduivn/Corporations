﻿using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

class ProductMoveClientsAtPeriodEnd : OnMonthChange
{
    public ProductMoveClientsAtPeriodEnd(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = CompanyUtils.GetProductCompanies(gameContext);

        foreach (var e in Products)
        {
            var churnClients = MarketingUtils.GetChurnClients(contexts.game, e.company.Id);

            var clients = Mathf.Max(0, e.marketing.clients - churnClients);

            e.ReplaceMarketing((long)clients);
        }
    }
}
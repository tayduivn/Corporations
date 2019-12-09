﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupStatsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyGrowthPreview>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughterCompanies(GameContext, MyCompany.company.Id)
            //.OrderBy(d => CompanyStatisticsUtils.GetIncomeGrowthAbsolute(d, 12))
            .OrderByDescending(d => EconomyUtils.GetCompanyCost(GameContext, d.company.Id))
            .ToArray();

        SetItems(daughters);
    }
}

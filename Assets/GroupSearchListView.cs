﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupSearchListView : ListView
{
    public GrowthFilterQuarterly GrowthFilterQuarterly;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyTableView>().SetEntity(e, data);
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var groups = Companies.GetGroupCompanies(GameContext)
            .OrderByDescending(g => Economy.GetCompanyCost(GameContext, g.company.Id))
            .ToArray();

        SetItems(groups, GrowthFilterQuarterly.Quarterly);
    }
}

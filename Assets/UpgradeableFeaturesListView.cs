﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeableFeaturesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var features = Products.GetUpgradeableRetentionFeatures(Flagship);

        SetItems(features);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}

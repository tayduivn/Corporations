﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAudiencesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<SegmentPreview>().SetEntity(entity as AudienceInfo, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        SetItems(audiences);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
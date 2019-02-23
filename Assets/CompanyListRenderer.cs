﻿using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

public class CompanyListRenderer : ListRenderer
{
    public override int itemsPerLine
    {
        get { return 3; }
        set { }
    }

    public override Vector2 spacing {
        get { return new Vector2(280f, 220f); }
        set { }
    }

    public override void RenderObject(GameObject obj, object item, int index, Dictionary<string, object> parameters)
    {
        ProductComponent project = (ProductComponent)item;
        int myCompanyId = (int)parameters["myCompanyId"];
        obj.GetComponentInChildren<CompanyView>().Render(project, myCompanyId);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

﻿using Unity.Entities;
using UnityEngine.UI;

public class ProductUpgrader : ComponentSystem
{
    struct Components
    {
        public ProductLevel ProductLevel;
        public Text text;
    }

    protected override void OnUpdate()
    {
        //foreach (var e in GetEntities<Components>())
        //{
        //    Debug.Log("OnUpdate Entities" + e.ProductLevel.level);

        //    e.ProductLevel.level++;
        //    e.text.text = e.ProductLevel.level + "";
        //}
    }
}
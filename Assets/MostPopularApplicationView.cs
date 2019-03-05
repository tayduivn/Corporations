﻿using UnityEngine.UI;
using Entitas;
using System;

public class MostPopularApplicationView : View
{
    Text MarketRequirements;
    Hint Hint;

    GameEntity GetLeaderApp()
    {
        var allProducts = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product));
        var myNicheProducts = Array.FindAll(allProducts, p => p.product.Niche == myProduct.Niche);

        GameEntity best = myProductEntity;

        foreach (var p in myNicheProducts)
        {
            if (p.marketing.Clients > best.marketing.Clients)
                best = p;
        }

        return best;
    }

    void Render()
    {
        var bestApp = GetLeaderApp();

        AnimateIfValueChanged(MarketRequirements, bestApp.product.Name);

        Hint.SetHint($"{bestApp.marketing.Clients} clients");
    }

    private void Start()
    {
        MarketRequirements = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }
}

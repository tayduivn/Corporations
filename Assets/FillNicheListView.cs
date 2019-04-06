﻿using Assets.Utils;
using Entitas;
using System;

public class FillNicheListView : View, IMenuListener
{
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    Predicate<GameEntity> FilterNichesByIndustry(IndustryType industry)
    {
        return n => n.niche.IndustryType == industry && n.niche.NicheType != NicheType.None;
    }

    GameEntity[] GetNiches()
    {
        var niches = GameContext.GetEntities(GameMatcher.Niche);

        IndustryType industryType = MenuUtils.GetIndustry(GameContext);

        return Array.FindAll(niches, FilterNichesByIndustry(industryType));
    }

    void Render()
    {
        var niches = GetNiches();

        GetComponent<NicheListView>().SetItems(niches);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        Render();
    }
}

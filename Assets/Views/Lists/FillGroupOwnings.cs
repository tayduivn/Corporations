﻿using Assets.Core;

public class FillGroupOwnings : View
    //, IAnyShareholdersListener
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<OwningsListView>().SetItems(GetOwnings());
    }

    GameEntity[] GetOwnings()
    {
        if (!HasCompany)
            return new GameEntity[0];

        return Companies.GetDaughters(MyGroupEntity, Q);
    }
}

﻿using Assets.Utils;

public class HideMainLinkButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsHasDaughters(GameContext, MyCompany) && CurrentIntDate < 180;
    }
}

﻿using Assets.Utils;

public class ReleaseAppButtonChecker : ResourceChecker
{
    public override string GetBaseHint()
    {
        return "";
    }

    public override TeamResource GetRequiredResources()
    {
        return new TeamResource();
    }
}

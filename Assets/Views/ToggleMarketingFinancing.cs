﻿using Assets.Core;

public class ToggleMarketingFinancing : ButtonView
{
    int companyId;
    
    public void SetCompanyId(int companyId)
    {
        this.companyId = companyId;
    }

    public override void Execute()
    {
        var company = Companies.Get(Q, companyId);
    }

    private void Start()
    {
        var company = Companies.Get(Q, companyId);

        ToggleIsChosenComponent(true);
    }
}

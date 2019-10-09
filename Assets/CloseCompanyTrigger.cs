﻿using Assets.Utils;

public class CloseCompanyTrigger : ButtonController
{
    public override void Execute()
    {
        var popup = new PopupMessageCompanyClose(SelectedCompany.company.Id);

        NotificationUtils.AddPopup(GameContext, popup);
    }
}

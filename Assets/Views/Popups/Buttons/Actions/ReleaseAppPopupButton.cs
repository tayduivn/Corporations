﻿using Assets.Core;

public class ReleaseAppPopupButton : PopupButtonController<PopupMessageDoYouWantToRelease>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;

        var company = Companies.Get(Q, companyId);
        Marketing.ReleaseApp(Q, company);


        NotificationUtils.ClosePopup(Q);

        NotificationUtils.AddPopup(Q, new PopupMessageRelease(companyId));
    }

    public override string GetButtonName() => "YES";
}

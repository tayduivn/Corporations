﻿using UnityEngine.UI;

public class RenderInvestmentRoundName : View
{
    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        GetComponent<Text>().text = "Round: " + SelectedCompany.investmentRounds.InvestmentRound;
    }
}

﻿using UnityEngine;
using UnityEngine.UI;

public class StartCampaignButton : ButtonController
{
    NicheType NicheType;
    //IndustryType Industry;
    public InputField Input;

    public GameObject CampaignButton;

    public override void Execute()
    {
        if (Input.text.Length == 0)
            return;

        CampaignButton.GetComponent<CampaignStarter>().Execute2(Input.text);
        //State.StartNewCampaign(Q, NicheType, Input.text);
    }

    //public void SetNiche(NicheType nicheType, InputField Input)
    //{
    //    NicheType = nicheType;
    //    this.Input = Input;
    //}

    //public void SetIndustry(IndustryType industry, InputField Input)
    //{
    //    var niches = Markets.GetPlayableNichesInIndustry(industry, Q).Where(m => Markets.IsAppropriateStartNiche(m, Q)).ToArray();
    //    var index = Random.Range(0, niches.Count());

    //    Debug.Log("Possible markets: " + string.Join(",", niches.Select(n => n.ToString())));
    //    Debug.Log("Setting industry: " + industry + " " + index + " " + niches.Count());

    //    var niche = niches[index].niche.NicheType;

    //    SetNiche(niche, Input);
    //}
}
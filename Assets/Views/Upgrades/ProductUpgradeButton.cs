﻿using Assets.Core;
using Michsky.UI.Frost;
using UnityEngine;

public abstract class ProductUpgradeButton : UpgradedButtonController
{
    public abstract ProductUpgrade upgrade { get; }

    public abstract string GetButtonTitle();
    public abstract string GetBenefits();
    public override bool IsInteractable() => true;

    public long GetCost() => 0; // Products.GetUpgradeCost(Company, Q, upgrade);
    public long GetAmountOfWorkers() => 0; // Products.GetUpgradeWorkerAmount(Company, Q, upgrade);

    bool state => Products.IsUpgradeEnabled(Company, upgrade);
    GameEntity Company => Flagship;

    public override void Execute()
    {
        if (Company != null)
        {
            Debug.Log("Toggle " + upgrade + " = " + state);

            //Products.SetUpgrade(Flagship, upgrade, Q, !state);
            ////flagship.productUpgrades.upgrades[upgrade] = !state;

            UpdatePage();
        }
    }

    void RenderToggleState(bool state, ToggleAnim anim)
    {
        anim.toggleAnimator.Play(state ? anim.toggleOn : anim.toggleOff);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var links = GetComponent<ProductUpgradeLinks>();

        if (links == null)
            return;

        // checkbox text
        links.Title.text = GetButtonTitle() + "\n" + GetBenefits();

        
        // proper animation
        links.Toggle.isOn = state;
        var anim = links.ToggleAnim;

        if (!TutorialUtils.IsDebugMode())
            RenderToggleState(state, anim);

        // hint
        var cost = GetCost() * C.PERIOD / 30;
        var text = $"{GetButtonTitle()}\n\n{GetBenefits()}\n\n";

        if (cost != 0)
        {
            text += "Cost: " + Visuals.Colorize(Format.Money(cost), Economy.IsCanMaintain(MyCompany, Q, cost));
        }

        var workers = GetAmountOfWorkers();
        if (workers > 0)
        {
            text += $"\n\nWill need {workers} additional workers";
        }

        links.Hint.SetHint("");
        links.CanvasGroup.alpha = state ? 1f : 0.25f;
    }
};

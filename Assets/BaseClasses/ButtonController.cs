﻿using Assets.Utils;
using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour, IEventGenerator
{
    public GameContext GameContext;
    public ProductComponent ControlledProduct;
    public GameEntity ControlledProductEntity;

    public NicheType SelectedNiche;

    Button Button;

    public abstract void Execute();

    void Awake()
    {
        GameContext = Contexts.sharedInstance.game;
    }

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    void Update()
    {
        UpdateControlledProductEntity();
    }

    void OnDestroy()
    {
        RemoveListener();
    }

    void UpdateControlledProductEntity()
    {
        ControlledProductEntity = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0];
        ControlledProduct = ControlledProductEntity.product;
    }

    //public GameEntity StartTask()
    //{
    //    return ControlledProductEntity;
    //}

    //public GameEntity SendEvent()
    //{
    //    // you can attach events to this object
    //    return GameContext.CreateEntity();
    //}

    void RemoveListener()
    {
        Button.onClick.RemoveListener(Execute);
    }

    public void Navigate(ScreenMode screenMode)
    {
        MenuUtils.GetMenu(GameContext).ReplaceMenu(screenMode);
    }

    public void TriggerEventUpgradeProduct(int productId, int ProductLevel)
    {
        ControlledProductEntity.AddEventUpgradeProduct(productId, ProductLevel);
    }

    public void TriggerEventTargetingToggle(int productId)
    {
        ControlledProductEntity.AddEventMarketingEnableTargeting(productId);
    }

    public void TriggerEventIncreasePrice(int productId)
    {
        TriggerEventChangePrice(productId, 1);
    }

    public void TriggerEventDecreasePrice(int productId)
    {
        TriggerEventChangePrice(productId, -1);
    }

    void TriggerEventChangePrice(int productId, int change)
    {
        int price = ControlledProductEntity.finance.price;

        ControlledProductEntity.AddEventFinancePricingChange(productId, price, change);
    }

    public void SetSelectedCompany(int companyId)
    {
        GameContext.GetEntities(GameMatcher.SelectedCompany)[0].isSelectedCompany = false;

        var company = Array.Find(GameContext.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);

        company.isSelectedCompany = true;
    }

    public void SetSelectedNiche(NicheType niche)
    {
        SelectedNiche = niche;
    }
}

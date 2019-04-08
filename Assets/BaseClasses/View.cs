﻿using Assets.Utils;
using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public GameEntity myProductEntity
    {
        get
        {
            return CompanyUtils.GetPlayerControlledProductCompany(GameContext);
        }
    }

    public GameEntity SelectedCompany
    {
        get
        {
            var data = MenuUtils.GetMenu(GameContext).menu.Data;

            if (data == null)
                return CompanyUtils.GetAnyOfControlledCompanies(GameContext);

            return CompanyUtils.GetCompanyById(GameContext, (int)data);
        }
    }
    
    public GameContext GameContext
    {
        get
        {
            return Contexts.sharedInstance.game;
        }
    }

    public ProductComponent MyProduct
    {
        get
        {
            return myProductEntity?.product;
        }
    }

    public int CurrentIntDate
    {
        get
        {
            return ScheduleUtils.GetCurrentDate(GameContext);
        }
    }

    public GameEntity[] GetCompetitors()
    {
        return CompanyUtils.GetMyCompetitors(GameContext);
    }

    public GameEntity[] GetNeighbours()
    {
        GameEntity[] products = CompanyUtils.GetProductsNotControlledByPlayer(GameContext);

        return Array.FindAll(products, e => e.product.Niche != MyProduct.Niche && e.product.Industry == MyProduct.Industry);
    }


    public float GetTaskCompletionPercentage(TaskComponent taskComponent)
    {
        return (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;
    }



    GameEntity[] GetTasks(TaskType taskType)
    {
        // TODO: add filtering tasks, which are done by other players!

        GameEntity[] gameEntities = GameContext
            .GetEntities(GameMatcher.Task);

        return Array.FindAll(gameEntities, e => e.task.TaskType == taskType);
    }

    public TaskComponent GetTask(TaskType taskType)
    {
        GameEntity[] tasks = GetTasks(taskType);

        if (tasks.Length == 0)
            return null;

        return tasks[0].task;
    }


    public void AnimateIfValueChanged(Text text, string value)
    {
        if (!String.Equals(text.text, value))
        {
            text.text = value;

            // only add this component if there is any
            if (text.gameObject.GetComponent<TextBlink>() == null)
                text.gameObject.AddComponent<TextBlink>();
        }
    }

    public void ListenMenuChanges(IMenuListener menuListener)
    {
        MenuUtils.GetMenu(GameContext).AddMenuListener(menuListener);
    }
}

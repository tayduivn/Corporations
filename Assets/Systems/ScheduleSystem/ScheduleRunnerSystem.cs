﻿using Assets;
using Assets.Core;
using Entitas;
using UnityEngine;

public class ScheduleRunnerSystem : IExecuteSystem
{
    // TODO TIMER
    readonly GameContext gameContext;
    float totalTime;

    bool isTimerRunning => DateEntity.isTimerRunning;
    int currentSpeed => DateEntity.speed.Speed;

    GameEntity DateContainer;

    GameEntity DateEntity
    {
        get
        {
            if (DateContainer == null || !DateContainer.hasDate)
                DateContainer = ScheduleUtils.GetDateContainer(gameContext);

            return DateContainer;
        }
    }

    public ScheduleRunnerSystem(Contexts contexts)
    {
        gameContext = contexts.game;
    }

    public void Execute()
    {
        if (DateEntity == null)
            return;

        CheckPressedButtons();

        totalTime -= Time.deltaTime;

        if (totalTime < 0 && isTimerRunning)
        {
            var playerCompany = Companies.GetPlayerCompany(gameContext);

            var profit = Economy.GetProfit(gameContext, playerCompany, true);
            var balance = Economy.BalanceOf(playerCompany);

            Debug.Log("BANKRUPTCY THREAT profit: " + profit.ToString());
            Debug.Log("BANKRUPTCY THREAT Balance: " + Format.Money(balance));

            while (ScheduleUtils.IsLastDayOfPeriod(DateEntity) && Economy.IsWillBecomeBankruptOnNextPeriod(gameContext, playerCompany))
            {
                if (playerCompany.isAutomaticInvestments && !Economy.IsHasCashOverflow(gameContext, playerCompany))
                {
                    Economy.RaiseFastCash(gameContext, playerCompany);
                    continue;
                }

                TutorialUtils.Unlock(gameContext, TutorialFunctionality.CanRaiseInvestments);
                TutorialUtils.Unlock(gameContext, TutorialFunctionality.BankruptcyWarning);
                NotificationUtils.AddPopup(gameContext, new PopupMessageBankruptcyThreat(playerCompany.company.Id));
                ScheduleUtils.PauseGame(gameContext);
                return;
            }

            // ResetTimer();
            totalTime = 1 / (float) currentSpeed;

            ScheduleUtils.IncreaseDate(gameContext, 1);
        }
    }

    void CheckPressedButtons()
    {
        // on right click
        // on right mouse click
        // on right mouse button
        if (Input.GetMouseButtonUp(1))
            ToggleTimer();

        //if (Input.GetKeyUp(KeyCode.Space))
        //    ToggleTimer();

        //if (Input.GetKeyUp(KeyCode.KeypadPlus) && currentSpeed < 18)
        //    UpdateSpeed(2);
        ////currentSpeed += 2;

        //if (Input.GetKeyUp(KeyCode.KeypadMinus) && currentSpeed > 2)
        //    UpdateSpeed(-1);
        //    //currentSpeed--;
    }

    void ToggleTimer()
    {
        ScheduleUtils.ToggleTimer(gameContext);
    }
}

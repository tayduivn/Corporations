﻿using Assets.Utils;

public enum WorkerType
{
    Programmer,
    Manager,
    Marketer
}

public class Hire : ButtonController
{
    public WorkerType worker;

    public override void Execute()
    {
        switch (worker)
        {
            case WorkerType.Manager:
                TeamUtils.HireManager(MyProductEntity);
                break;
            case WorkerType.Marketer:
                TeamUtils.HireMarketer(MyProductEntity);
                break;
            case WorkerType.Programmer:
                TeamUtils.HireProgrammer(MyProductEntity);
                break;
        }
    }
}

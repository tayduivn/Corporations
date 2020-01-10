﻿using Assets.Core;
using Assets.Core.Formatting;
using UnityEngine.UI;

public partial class TaskView : View
{
    public Text Text;

    TaskComponent TaskComponent;

    public void SetEntity(TaskComponent task)
    {
        TaskComponent = task;
        AddLinkToObservableObject(task.CompanyTask);

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        var text = GetTaskHeader(TaskComponent.CompanyTask) + "\n\n";

        var remaining = TaskComponent.EndTime - CurrentIntDate;

        if (remaining <= 0)
        {
            text += "DONE";
        }
        else
        {
            text += remaining + " days left";
            if (!ScheduleUtils.IsTimerRunning(GameContext))
                text += ". " + Visuals.Negative("Unpause") + " to finish";
        }

        Text.text = text;
    }

    private void AddLinkToObservableObject(CompanyTask companyTask)
    {
        //switch (companyTask.CompanyTaskType)
        //{
        //    case CompanyTaskType.ExploreMarket:
        //        AddIfAbsent<LinkToNiche>().SetNiche((companyTask as CompanyTaskExploreMarket).NicheType);
        //        break;

        //    case CompanyTaskType.ExploreCompany:
        //        AddIfAbsent<LinkToProjectView>().CompanyId = ((companyTask as CompanyTaskExploreCompany).CompanyId);
        //        break;

        //    case CompanyTaskType.AcquiringCompany:
        //        AddIfAbsent<LinkToProjectView>().CompanyId = ((companyTask as CompanyTaskAcquisition).CompanyId);
        //        break;
        //}
    }
}

public partial class TaskView : View
{
    string GetTaskString(CompanyTask companyTask)
    {
        var text = companyTask.CompanyTaskType.ToString();

        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return text + (companyTask as CompanyTaskAcquisition).CompanyId;

            case CompanyTaskType.ExploreMarket:
                return text + (companyTask as CompanyTaskExploreMarket).NicheType;

            case CompanyTaskType.ExploreCompany:
                return text + (companyTask as CompanyTaskExploreCompany).CompanyId;

            default: return "UNKNOWN TASK!!!!" + text;
        }
    }
}

public partial class TaskView : View
{
    string GetTaskHeader(CompanyTask companyTask)
    {
        switch (companyTask.CompanyTaskType)
        {
            case CompanyTaskType.AcquiringCompany:
                return "Acquiring company\n" + Companies.GetCompany(GameContext, (companyTask as CompanyTaskAcquisition).CompanyId).company.Name;

            case CompanyTaskType.ExploreMarket:
                return "Exploring new market\n" + EnumUtils.GetFormattedNicheName((companyTask as CompanyTaskExploreMarket).NicheType);

            case CompanyTaskType.ExploreCompany:
                return "Exploring company\n" + Companies.GetCompany(GameContext, (companyTask as CompanyTaskExploreCompany).CompanyId).company.Name;

            default: return "UNKNOWN TASK!!!!" + companyTask.CompanyTaskType;
        }
    }
}


public enum CompanyTaskType
{
    ExploreMarket,
    ExploreCompany,

    AcquiringCompany,
    AcquiringParlay,


}

public abstract class CompanyTask
{
    public CompanyTaskType CompanyTaskType;

    public bool Equals(CompanyTask obj)
    {
        return CompanyTaskType == obj.CompanyTaskType && EqualsExactly(obj);
    }
    public abstract bool EqualsExactly(CompanyTask obj);
}


public class CompanyTaskAcquisition : CompanyTask
{
    public int CompanyId;

    public CompanyTaskAcquisition(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.AcquiringCompany;
    }

    public override bool EqualsExactly(CompanyTask obj)
    {
        return CompanyId == (obj as CompanyTaskAcquisition).CompanyId;
    }
}

public class CompanyTaskExploreCompany : CompanyTask
{
    public int CompanyId;

    public CompanyTaskExploreCompany(int companyId)
    {
        CompanyTaskType = CompanyTaskType.ExploreCompany;
        CompanyId = companyId;
    }

    public override bool EqualsExactly(CompanyTask obj)
    {
        return CompanyId == (obj as CompanyTaskExploreCompany).CompanyId;
    }
}

// markets
public class CompanyTaskExploreMarket : CompanyTask
{
    public NicheType NicheType;

    public CompanyTaskExploreMarket(NicheType nicheType)
    {
        CompanyTaskType = CompanyTaskType.ExploreMarket;
        NicheType = nicheType;
    }

    public override bool EqualsExactly(CompanyTask obj)
    {
        return NicheType == (obj as CompanyTaskExploreMarket).NicheType;
    }
}

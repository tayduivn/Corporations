﻿using Entitas;

public interface IEventGenerator
{
    void TriggerEventUpgradeProduct(int productId, int ProductLevel);
    void TriggerEventUpgradeAnalytics(int productId);
    void TriggerEventTargetingToggle(int productId);
}

[Game]
public class EventComponent : IComponent { }

#region Product specific events

[Game]
public class EventUpgradeProductComponent : IComponent
{
    public int productId;
    public int previousLevel;
}

[Game]
public class EventMarketingStartSimpleCampaign : IComponent
{
    public int productId;
}

[Game]
public class EventUpgradeAnalyticsComponent : IComponent
{
    public int productId;
}

[Game]
public class EventMarketingEnableTargetingComponent : IComponent
{
    public int productId;
}

[Game]
public class EventStaffHireProgrammerComponent : IComponent
{
    public int productId;
}

#endregion

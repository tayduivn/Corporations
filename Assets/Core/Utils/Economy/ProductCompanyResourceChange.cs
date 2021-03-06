﻿namespace Assets.Core
{
    static partial class Economy
    {
        public static TeamResource GetProductCompanyResourceChange(GameEntity company, GameContext gameContext)
        {
            long money = GetProfit(gameContext, company);
            var ideas = Products.GetExpertiseGain(company);

            return new TeamResource(
                0,
                0,
                0,
                ideas,
                money
                );
        }
    }
}

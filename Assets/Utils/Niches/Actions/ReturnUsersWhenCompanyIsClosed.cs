﻿using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static void ReturnUsersWhenCompanyIsClosed(GameEntity e, GameContext gameContext)
        {
            var users = MarketingUtils.GetClients(e);

            var niche = GetNiche(gameContext, e.product.Niche);

            var companies = GetProductsOnMarket(gameContext, e.company.Id);

            var powers = companies.Sum(c => c.branding.BrandPower + 1) - e.branding.BrandPower;

            foreach (var c in companies)
            {
                if (c == e)
                    continue;

                var part = (long)((1 + c.branding.BrandPower) * users / powers);
                MarketingUtils.AddClients(c, part);
            }

            MarketingUtils.AddClients(e, -users);
        }
    }
}
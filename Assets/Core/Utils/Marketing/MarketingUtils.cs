﻿using System.Linq;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetClients(GameEntity company)
        {
            return company.marketing.ClientList.Values.Sum();
            return company.marketing.clients;
        }

        public static long GetClients(GameEntity company, int segmentId)
        {
            return company.marketing.ClientList.ContainsKey(segmentId) ? company.marketing.ClientList[segmentId] : 0;
            return company.marketing.clients;
        }

        public static void AddClients(GameEntity company, long clients, int segmentId)
        {
            var marketing = company.marketing;

            if (!marketing.ClientList.ContainsKey(segmentId))
                marketing.ClientList[segmentId] = 0;

            marketing.ClientList[segmentId] += clients;

            if (marketing.ClientList[segmentId] < 0)
            {
                marketing.ClientList[segmentId] = 0;
            }

            company.ReplaceMarketing(marketing.ClientList.Values.Sum(), marketing.ClientList);
            //company.ReplaceMarketing(marketing.clients + clients);
        }

        public static void LoseClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            var newClients = marketing.clients - clients;
            if (newClients < 0)
                newClients = 0;

            //company.ReplaceMarketing(newClients);
        }

        public static long GetChurnClients(GameContext gameContext, int companyId, int segmentId) => GetChurnClients(gameContext, Companies.Get(gameContext, companyId), segmentId);
        public static long GetChurnClients(GameContext gameContext, GameEntity c, int segmentId)
        {
            var churn = GetChurnRate(gameContext, c.company.Id, segmentId);

            var clients = GetClients(c, segmentId);

            return clients * churn / 100;
        }

        public static void ReleaseApp(GameContext gameContext, int companyId) => ReleaseApp(gameContext, Companies.Get(gameContext, companyId));
        public static void ReleaseApp(GameContext gameContext, GameEntity product)
        {
            if (!product.isRelease)
            {
                AddBrandPower(product, C.RELEASE_BRAND_POWER_GAIN);
                var flow = GetClientFlow(gameContext, product.product.Niche);

                AddClients(product, flow, product.productPositioning.Positioning);

                product.isRelease = true;
                Investments.CompleteGoal(product, gameContext);
            }
        }
    }
}

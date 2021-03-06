﻿using System.Linq;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static void UpdateMarketRequirements(GameEntity product, GameContext gameContext)
        {
            var niche = Markets.Get(gameContext, product.product.Niche);

            var demand = Products.GetMarketDemand(niche);
            var newLevel = Products.GetProductLevel(product);

            if (newLevel > demand)
            {
                bool revolution = newLevel - demand > 1;

                // innovation
                //var clientChange = GiveInnovationBenefits(product, gameContext, revolution);
                var brandGain = revolution ? C.REVOLUTION_BRAND_POWER_GAIN : C.INNOVATION_BRAND_POWER_GAIN;
                Marketing.AddBrandPower(product, brandGain);

                // notify about innovation
                var player = Companies.GetPlayerCompany(gameContext);
                var daughters = Companies.GetDaughtersAmount(player);


                //if (Companies.IsInPlayerSphereOfInterest(product, gameContext) && Markets.GetCompetitorsAmount(product, gameContext) > 1 && daughters == 1)
                //    NotificationUtils.AddPopup(gameContext, new PopupMessageInnovation(product.company.Id, clientChange));

                // && Markets.GetCompetitorsAmount(product, gameContext) > 1 && daughters == 1
                if (Companies.IsInPlayerSphereOfInterest(product, gameContext))
                    NotificationUtils.AddNotification(gameContext, new NotificationMessageInnovation(product.company.Id, newLevel, revolution, 0, brandGain));

                niche.ReplaceSegment(newLevel);

                // order matters
                RemoveTechLeaders(product, gameContext);
                product.isTechnologyLeader = true;
            }
            else if (newLevel == demand)
            {
                // if you are techonology leader and you fail to innovate, you will not lose tech leadership
                if (product.isTechnologyLeader)
                    return;

                RemoveTechLeaders(product, gameContext);
            }
        }


        private static void RemoveTechLeaders(GameEntity product, GameContext gameContext)
        {
            var players = Markets.GetProductsOnMarket(gameContext, product).ToArray();

            foreach (var p in players)
                p.isTechnologyLeader = false;
        }


        private static long GiveInnovationBenefits(GameEntity product, GameContext gameContext, bool revolution)
        {
            long sum = 0;

            if (revolution)
            {
                // get your competitor's clients
                var innovatorCompetitors = Markets.GetProductsOnMarket(gameContext, product)
                    .Where(p => p.isRelease)
                    .Where(p => p.company.Id != product.company.Id);


                foreach (var p in innovatorCompetitors)
                {
                    var disloyal = Marketing.GetUsers(p) / 15;

                    //Marketing.LoseClients(p, disloyal);
                    //Marketing.AddClients(product, disloyal);

                    sum += disloyal;
                }

            }

            return sum;
        }
    }
}

﻿namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetMarketDemand(GameEntity niche)
        {
            return niche.segment.Level;
        }

        public static int GetMarketRequirements(GameEntity niche) => GetMarketDemand(niche);
        public static int GetMarketRequirements(GameEntity product, GameContext gameContext)
        {
            var niche = Markets.Get(gameContext, product.product.Niche);

            return GetMarketDemand(niche);
        }

        // always positive or equal to zero
        public static int GetDifferenceBetweenMarketDemandAndAppConcept(GameEntity product, GameContext gameContext)
        {
            var niche = Markets.Get(gameContext, product.product.Niche);

            var demand = GetMarketDemand(niche);
            var level = GetProductLevel(product);

            return demand - level;
        }


        public static bool IsInMarket(GameEntity product, GameContext gameContext)
        {
            return GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext) == 0;
        }

        public static bool IsOutOfMarket(GameEntity product, GameContext gameContext)
        {
            return GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext) > 0;
        }

        public static bool IsWillInnovate(GameEntity product, GameContext gameContext)
        {
            return IsInMarket(product, gameContext);
        }


        public static ConceptStatus GetConceptStatus(GameEntity product, GameContext gameContext)
        {
            var isRelevant = IsInMarket(product, gameContext);
            var isOutdated = IsOutOfMarket(product, gameContext);

            if (product.isTechnologyLeader)
                return ConceptStatus.Leader;

            if (isOutdated)
                return ConceptStatus.Outdated;

            return ConceptStatus.Relevant;
        }
    }
}

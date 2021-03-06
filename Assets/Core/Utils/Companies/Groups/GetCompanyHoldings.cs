﻿using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static GameEntity[] GetDaughterProducts(GameContext context, GameEntity company)
        {
            return GetDaughters(company, context)
                .Where(p => p.hasProduct)
                .ToArray();
        }

        public static bool IsHasReleasedProducts(GameContext gameContext, GameEntity company)
        {
            return GetDaughterProducts(gameContext, company)
                .Count(p => p.isRelease) > 0;
        }

        public static GameEntity[] GetDaughtersOnMarket(GameEntity group, NicheType nicheType, GameContext gameContext)
        {
            return GetDaughterProducts(gameContext, group)
                .Where(p => p.product.Niche == nicheType)
                .ToArray();
        }

        public static bool IsHasDaughters(GameEntity company)
        {
            return GetDaughtersAmount(company) > 0;
        }

        public static int GetDaughtersAmount(GameEntity company)
        {
            return company.hasOwnings ? company.ownings.Holdings.Count() : 0;
        }

        public static GameEntity[] GetDaughters(GameEntity c, GameContext context)
        {
            return Investments.GetOwnings(c, context);
        }

        public static bool IsDaughterOf(GameEntity parent, GameEntity daughter)
        {
            if (!parent.hasShareholder)
            {
                Companies.Log(daughter, $"FALSELY CALLED IS_DAUGHTER_OF on companies: possible parent={parent.company.Name}, possible daughter={daughter.company.Name}");
                return false;
            }

            return Investments.IsInvestsInCompany(parent, daughter); // IsInvestsInCompany(daughter, parent.shareholder.Id);
        }

        public static GameEntity GetParentCompany(GameContext context, GameEntity company)
        {
            if (company.isIndependentCompany)
                return null;


            var shares = 0;
            var shareholders = company.shareholders.Shareholders;
            GameEntity parent = null;

            foreach (var shareholder in shareholders)
            {
                var id = shareholder.Key;
                var block = shareholder.Value.amount;

                var investor = Investments.GetInvestor(context, id);

                if (investor.hasCompany)
                {
                    // is managing company
                    if (block > shares)
                    {
                        parent = investor;
                    }
                }
            }

            return parent;
        }

        // changes
        public static ProductCompanyResult GetProductCompanyResults(GameEntity product, GameContext gameContext)
        {
            var competitors = Markets.GetProductsOnMarket(gameContext, product);

            long previousMarketSize = 0;
            long currentMarketSize = 0;

            long prevCompanyClients = 0;
            long currCompanyClients = 0;

            foreach (var c in competitors)
            {
                var last = c.metricsHistory.Metrics.Count - 1;
                var prev = c.metricsHistory.Metrics.Count - 2;

                // company was formed this month
                if (prev < 0)
                    continue;

                var audience = c.metricsHistory.Metrics[prev].AudienceSize;
                var clients = c.metricsHistory.Metrics[last].AudienceSize;

                previousMarketSize += audience;
                currentMarketSize += clients;

                if (c.company.Id == product.company.Id)
                {
                    prevCompanyClients = audience;
                    currCompanyClients = clients;
                }
            }

            var prevShare = prevCompanyClients * 100d / (previousMarketSize + 1);
            var Share = currCompanyClients * 100d / (currentMarketSize + 1);

            return new ProductCompanyResult
            {
                clientChange = currCompanyClients - prevCompanyClients,
                MarketShareChange = (float)(Share - prevShare),
                ConceptStatus = Products.GetConceptStatus(product, gameContext),
                CompanyId = product.company.Id
            };
        }
    }
}

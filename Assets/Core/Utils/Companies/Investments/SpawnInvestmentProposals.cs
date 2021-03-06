﻿using System;

namespace Assets.Core
{
    partial class Companies
    {
        public static void SpawnProposals(GameContext context, GameEntity company)
        {
            long cost = Economy.CostOf(company, context);
            var date = ScheduleUtils.GetCurrentDate(context);

            var potentialInvestors = GetPotentialInvestors(context, company);

            foreach (var potentialInvestor in potentialInvestors)
            {
                var modifier = 50 + UnityEngine.Random.Range(0, 100);

                long valuation = cost * modifier / 100;

                var max = GetMaxInvestingAmountForInvestorType(potentialInvestor);

                var ShareholderId = potentialInvestor.shareholder.Id;
                var Duration = 10; // UnityEngine.Random.Range(5, 10);

                // TODO increase offer on early stages
                // or increase company valuation instead!
                var offer = Math.Min(valuation / 20, max);

                //var goal = new InvestmentGoalUnknown(InvestorGoalType.GrowCompanyCost);
                var goal = company.companyGoal.Goals[0];


                var p = new InvestmentProposal
                {
                    Investment = new Investment(offer, Duration, goal, date),
                    AdditionalShares = (int)GetNewSharesSize(context, company, offer),

                    ShareholderId = ShareholderId,
                    WasAccepted = false
                };

                // you cannot invest in yourself!
                if (company.hasShareholder && company.shareholder.Id == ShareholderId)
                    continue;

                AddInvestmentProposal(company, p);
            }
        }

        public static long GetMaxInvestingAmountForInvestorType(GameEntity investor)
        {
            switch (investor.shareholder.InvestorType)
            {
                case InvestorType.FFF: return 10000;

                case InvestorType.Angel: return 150000;

                case InvestorType.VentureInvestor: return 1000000;

                case InvestorType.StockExchange: return 50000000;

                default: return 0;
            }
        }
    }
}

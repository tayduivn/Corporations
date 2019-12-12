﻿namespace Assets.Utils
{
    public static partial class Economy
    {
        public static long GetCompanyCost(GameContext context, GameEntity c)
        {
            long cost;
            if (Companies.IsProductCompany(c))
                cost = GetProductCompanyCost(context, c.company.Id);
            else
                cost = GetGroupOfCompaniesCost(context, c);

            long capital = c.companyResource.Resources.money;

            // +1 to avoid division by zero
            return cost + capital + 1;
        }

        public static long GetCompanyCost(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            return GetCompanyCost(context, c);
        }

        public static long GetCompanySellingPrice(GameContext context, int companyId)
        {
            var target = Companies.GetCompany(context, companyId);

            var desireToSell = Companies.GetDesireToSellCompany(target, context);

            return GetCompanyCost(context, companyId) * desireToSell;
        }

        public static long GetCompanyCostNicheMultiplier()
        {
            return 15;
        }

        public static long GetCompanyBaseCost(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            if (Companies.IsProductCompany(c))
                return GetProductCompanyBaseCost(context, companyId);

            return GetCompanyCost(context, companyId);
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = Companies.GetCompany(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostNicheMultiplier();
        }
    }
}

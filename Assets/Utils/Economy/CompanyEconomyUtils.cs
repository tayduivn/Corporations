﻿namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static long GetCompanyIncome(GameEntity e, GameContext context)
        {
            if (CompanyUtils.IsProductCompany(e))
                return GetProductCompanyIncome(e);

            return GetGroupIncome(context, e);
        }

        public static long GetCompanyIncome(int companyId, GameContext context)
        {
            var e = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(e, context);
        }


        public static long GetCompanyCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            if (CompanyUtils.IsCompanyGroupLike(c))
                return GetGroupOfCompaniesCost(context, c);

            return GetProductCompanyCost(context, companyId);
        }

        public static long GetCompanyCostEnthusiasm()
        {
            return 15;
        }

        public static long GetCompanyIncomeBasedCost(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetCompanyIncome(c, context) * GetCompanyCostEnthusiasm();
        }

        internal static long GetCompanyMaintenance(GameEntity c, GameContext gameContext)
        {
            return GetProductCompanyMaintenance(c);
        }

        internal static long GetBalanceChange(GameEntity c, GameContext context)
        {
            return GetCompanyIncome(c, context) - GetCompanyMaintenance(c, context);
        }

        internal static bool IsROICounable(GameEntity c, GameContext context)
        {
            return GetCompanyMaintenance(c, context) > 0;
        }

        internal static long GetBalanceROI(GameEntity c, GameContext context)
        {
            long maintenance = GetCompanyMaintenance(c, context);
            long change = GetBalanceChange(c, context);

            return change * 100 / maintenance;
        }
    }
}

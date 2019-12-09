﻿using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class Companies
    {
        internal static GameEntity[] GetIndependentCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.IndependentCompany, GameMatcher.Alive));
        }

        // products
        internal static GameEntity[] GetProductCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Product, GameMatcher.Alive));
        }

        internal static GameEntity[] GetAIProducts(GameContext gameContext)
        {
            return Array.FindAll(GetProductCompanies(gameContext),
                p => !IsCompanyRelatedToPlayer(gameContext, p)
                );
        }

        internal static GameEntity[] GetPlayerRelatedCompanies(GameContext gameContext)
        {
            return Array.FindAll(GetProductCompanies(gameContext),
                p => IsCompanyRelatedToPlayer(gameContext, p)
                );
        }

        // groups
        internal static GameEntity[] GetAIManagingCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.ManagingCompany, GameMatcher.Alive)
                .NoneOf(GameMatcher.ControlledByPlayer));
        }

        internal static GameEntity[] GetGroupCompanies(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.ManagingCompany, GameMatcher.Alive));
        }

        internal static GameEntity[] GetNonFinancialCompanies(GameContext gameContext)
        {
            var investableCompanies = gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.InvestmentProposals));

            return Array.FindAll(investableCompanies, IsNotFinancialStructure);
        }

        internal static GameEntity[] GetInvestmentFunds(GameContext gameContext)
        {
            var investingCompanies = gameContext.GetEntities(GameMatcher
                .AllOf(GameMatcher.Company, GameMatcher.Shareholder));

            return Array.FindAll(investingCompanies, IsFinancialStructure);
        }



        public static bool IsNotFinancialStructure(GameEntity e)
        {
            return !IsFinancialStructure(e);
        }

        public static bool IsFinancialStructure(GameEntity e)
        {
            return e.company.CompanyType == CompanyType.FinancialGroup;
        }
    }
}

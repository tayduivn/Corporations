﻿using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void SpendResources(GameEntity company, long money)
        {
            SpendResources(company, new TeamResource(money));
        }

        public static void SpendResources(GameEntity company, TeamResource resource)
        {
            //if (company.company.Name == "Windows")
            //{
            //    Debug.Log("Spending: " + resource.ToString());
            //}
            company.companyResource.Resources.Spend(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
        }

        public static void SetResources(GameEntity company, TeamResource resource)
        {
            company.ReplaceCompanyResource(resource);
        }

        public static void AddResources(GameEntity company, TeamResource resource)
        {
            company.companyResource.Resources.Add(resource);

            company.ReplaceCompanyResource(company.companyResource.Resources);
        }

        public static void SetStartCapital(GameEntity product, GameEntity niche)
        {
            var startCapital = NicheUtils.GetStartCapital(niche) * Random.Range(50, 150) / 100;

            AddResources(product, new TeamResource(startCapital));
        }

        public static void SetStartCapital(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            SetStartCapital(product, niche);
        }

        public static bool IsEnoughResources(GameEntity company, long money)
        {
            return IsEnoughResources(company, new TeamResource(money));
        }

        public static bool IsEnoughResources(GameEntity company, TeamResource resource)
        {
            return company.companyResource.Resources.IsEnoughResources(resource);
        }
    }
}

﻿using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static GameEntity GetNicheEntity(GameContext gameContext, NicheType nicheType)
        {
            var e = Array.Find(gameContext.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == nicheType);

            if (e == null)
                e = CreateNicheMockup(nicheType, gameContext);

            return e;
        }

        public static GameEntity CreateNicheMockup(NicheType niche, GameContext GameContext)
        {
            var e = GameContext.CreateEntity();

            e.AddNiche(
                niche,
                IndustryType.Communications,
                new List<MarketCompatibility>(),
                new List<NicheType>(),
                NicheType.SocialNetwork,
                0
                );

            e.AddNicheCosts(1, 1, 1, 1, 1, 1);

            e.AddNicheState(
                new Dictionary<NicheLifecyclePhase, int>
                {
                    [NicheLifecyclePhase.Idle] = 0, // 0
                    [NicheLifecyclePhase.Innovation] = UnityEngine.Random.Range(1, 4), // 2-5            Xt
                    [NicheLifecyclePhase.Trending] = UnityEngine.Random.Range(5, 10), // 4 - 10           5Xt
                    [NicheLifecyclePhase.MassUse] = UnityEngine.Random.Range(11, 15), // 7 - 15            10Xt
                    [NicheLifecyclePhase.Decay] = UnityEngine.Random.Range(2, 5), // 2 - 5 // churn      3Xt-22Xt
                    [NicheLifecyclePhase.Death] = 0, // churn
                },
                NicheLifecyclePhase.Innovation,
                0
                );

            e.AddNicheClientsContainer(new Dictionary<int, long>());
            e.AddNicheSegments(new Dictionary<int, ProductPositioning>());


            e.AddSegment(new Dictionary<UserType, int>
            {
                [UserType.Core] = 1,
                [UserType.Regular] = 1,
                [UserType.Mass] = 1,
            });

            return e;
        }

        public static NicheLifecyclePhase GetMarketState(GameContext gameContext, NicheType nicheType)
        {
            return GetNicheEntity(gameContext, nicheType).nicheState.Phase;
        }

        public static GameEntity[] GetIndustries(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Industry);
        }

        public static GameEntity[] GetNiches(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Niche);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetPlayersOnMarket(context, c);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, GameEntity e)
        {
            return GetPlayersOnMarket(context, e.product.Niche);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, NicheType niche)
        {
            return context.GetEntities(GameMatcher.Product).Where(p => p.product.Niche == niche);
        }

        public static GameEntity[] GetPlayersOnMarket(GameContext context, NicheType niche, bool something)
        {
            return Array.FindAll(
                context.GetEntities(GameMatcher.Product),
                p => p.product.Niche == niche
                );
        }

        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, e).Count();
        }

        public static int GetCompetitorsAmount(NicheType niche, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, niche).Count();
        }

        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }

        public static IEnumerable<string> GetCompetitorSegmentLevels(GameEntity e, GameContext context, UserType userType)
        {
            var names = GetPlayersOnMarket(context, e)
                .Select(c => c.product.Concept + "lvl - " + ProlongNameToNDigits(c.company.Name, 10));

            return names;
        }

        public static bool IsPerspectiveNiche(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            var phase = niche.nicheState.Phase;

            return phase == NicheLifecyclePhase.Innovation ||
                phase == NicheLifecyclePhase.Trending ||
                phase == NicheLifecyclePhase.MassUse;
        }

        public static bool IsPlayableNiche(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            return IsPlayableNiche(gameContext, niche);
        }

        public static bool IsPlayableNiche(GameContext gameContext, GameEntity niche)
        {
            var phase = niche.nicheState.Phase;

            return phase != NicheLifecyclePhase.Idle && phase != NicheLifecyclePhase.Death;
        }

        public static int GetMarketRating(GameContext gameContext, NicheType niche)
        {
            return GetMarketRating(GetNicheEntity(gameContext, niche));
        }

        public static int GetMarketRating(GameEntity niche)
        {
            switch (niche.nicheState.Phase)
            {
                case NicheLifecyclePhase.Idle: return 1;
                case NicheLifecyclePhase.Innovation: return 3;
                case NicheLifecyclePhase.Trending: return 4;
                case NicheLifecyclePhase.MassUse: return 5;
                case NicheLifecyclePhase.Decay: return 2;

                default:
                    return 0;
            }
        }

        internal static long GetMarketSize(GameContext gameContext, NicheType nicheType)
        {
            var products = NicheUtils.GetPlayersOnMarket(gameContext, nicheType);

            return products.Select(p => CompanyEconomyUtils.GetCompanyCost(gameContext, p.company.Id)).Sum();
            //return products.Select(p => CompanyEconomyUtils.GetProductCompanyBaseCost(gameContext, p.company.Id)).Sum();
        }

        public static long GetMarketPotential(GameContext gameContext, NicheType nicheType)
        {
            return GetMarketPotential(GetNicheEntity(gameContext, nicheType));
        }

        public static float GetSegmentProductPrice(GameContext gameContext, NicheType nicheType, int segmentId)
        {
            var positioningData = GetProductPositioningInfo(gameContext, nicheType, segmentId);

            var niche = GetNicheEntity(gameContext, nicheType);

            var priceModifier = positioningData.priceModifier;
            if (priceModifier == 0)
                priceModifier = 1;

            return niche.nicheCosts.BasePrice * priceModifier;
        }

        public static ProductPositioning GetProductPositioningInfo(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var positionings = GetNichePositionings(nicheType, GameContext);
            var niche = GetNicheEntity(GameContext, nicheType);

            return positionings[segmentId];
        }

        public static long GetMarketSegmentPotential(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var price = (long)(GetSegmentProductPrice(GameContext, nicheType, segmentId) * 100);

            return price * GetMarketSegmentAudiencePotential(GameContext, nicheType, segmentId) / 100;
        }

        public static long GetMarketSegmentAudiencePotential(GameContext GameContext, NicheType nicheType, int segmentId)
        {
            var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

            var positioningData = GetProductPositioningInfo(GameContext, nicheType, segmentId);

            return positioningData.marketShare * NicheUtils.GetMarketAudiencePotential(niche) / 100;
        }

        public static long GetMarketAudiencePotential(GameEntity niche)
        {
            var state = niche.nicheState;

            var clientBatch = niche.nicheCosts.ClientBatch;

            long clients = 0;

            foreach (var g in state.Growth)
            {
                var phasePeriod = GetMinimumPhaseDurationInPeriods(g.Key) * GetNichePeriodDuration(niche) * 30;

                var brandModifier = 1.5f;
                var financeReach = MarketingUtils.GetMarketingFinancingAudienceReachModifier(MarketingFinancing.High);

                clients += (long)(clientBatch * g.Value * phasePeriod * brandModifier * financeReach);
            }

            return clients;
        }

        public static long GetMarketPotential(GameEntity niche)
        {
            var clients = GetMarketAudiencePotential(niche);

            var price = niche.nicheCosts.BasePrice * 1.5f;

            return (long)(clients * CompanyEconomyUtils.GetCompanyCostNicheMultiplier() * price);
        }

        // months
        public static int GetNichePeriodDuration(GameEntity niche)
        {
            var X = 1;

            return X;
        }

        public static int GetMinimumPhaseDurationInPeriods(NicheLifecyclePhase phase)
        {
            if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Idle)
                return 0;

            return 1;

            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:
                    return 1;

                case NicheLifecyclePhase.Trending:
                    return 4;

                case NicheLifecyclePhase.MassUse:
                    return 10;

                case NicheLifecyclePhase.Decay:
                    return 15;

                default:
                    return 0;
            }
        }


        public static IndustryType GetIndustry(NicheType niche, GameContext context)
        {
            return Array.Find(context.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == niche).niche.IndustryType;
        }

        public static GameEntity[] GetNichesInIndustry(IndustryType industry, GameContext context)
        {
            return Array.FindAll(context.GetEntities(GameMatcher.Niche), n => n.niche.IndustryType == industry);
        }

        public static GameEntity[] GetPlayableNichesInIndustry(IndustryType industry, GameContext context)
        {
            var niches = GetNichesInIndustry(industry, context);
            return Array.FindAll(niches, n => IsPlayableNiche(context, n));
        }



        public static List<GameEntity> GetProductsAvailableForSaleInSphereOfInfluence(GameEntity managingCompany, GameContext context)
        {
            List<GameEntity> products = new List<GameEntity>();

            var niches = managingCompany.companyFocus.Niches;

            foreach (var n in niches)
            {
                var companies = GetProductsAvailableForSaleOnMarket(n, context);

                products.AddRange(companies);
            }

            return products.FindAll(p => !CompanyUtils.IsCompanyRelatedToPlayer(context, p));
        }

        public static GameEntity[] GetProductsAvailableForSaleOnMarket(NicheType n, GameContext context)
        {
            return GetPlayersOnMarket(context, n)
                .Where(p => CompanyUtils.IsWillSellCompany(p, context) && p.isAlive && p.companyGoal.InvestorGoal != InvestorGoal.Prototype)
                .ToArray();
        }
    }
}

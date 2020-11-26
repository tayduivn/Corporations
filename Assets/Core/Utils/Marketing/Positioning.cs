﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static ProductPositioning GetPositioning(GameEntity product)
        {
            var positionings = GetNichePositionings(product);

            var pos = product.productPositioning.Positioning;

            try
            {
                return positionings.First(p => p.ID == pos);
            }
            catch
            {
                Debug.LogError($"Get positioning bug in {product.company.Name}: index={pos}");

                return positionings[0];
            }
        }

        public static string GetPositioningName(GameEntity product)
        {
            return GetPositioning(product).name;
        }

        public static long GetAudienceWorth(AudienceInfo audienceInfo)
        {
            return audienceInfo.Size;
        }

        public static long GetPositioningWorth(GameEntity product, ProductPositioning productPositioning)
        {
            var audiences = GetAudienceInfos();

            return productPositioning.Loyalties
                .Select((l, i) => new { i, cost = GetAudienceWorth(audiences[i]), isLoyal = l >= 0 })
                .Sum(f => f.isLoyal ? f.cost : 0);
        }

        public static List<ProductPositioning> GetNichePositionings(GameEntity product)
        {
            return product.nicheSegments.Positionings;
        }

        public static void ChangePositioning(GameEntity product, GameContext gameContext, int positioningId)
        {
            var previousPositioning = product.productPositioning.Positioning;
            bool isChaningPositioning = previousPositioning != positioningId;

            if (!isChaningPositioning)
                return;

            var playerFlagship = Companies.GetPlayerFlagship(gameContext);

            if (Companies.IsDirectCompetitor(playerFlagship, product) && !product.isFlagship)
            {
                // was competitor
                NotificationUtils.AddSimplePopup(gameContext, Visuals.Positive($"Our competitor left competition!"), $"{product.company.Name} is no longer competing with us, cause they changed their product positioning");
            }



            product.productPositioning.Positioning = positioningId;


            if (product.isFlagship)
            {
                if (Marketing.GetUsers(product) > 50)
                {
                    var segments = Marketing.GetNichePositionings(product);
                    var audiences = GetAudienceInfos();

                    var newLoyalties = segments[positioningId].Loyalties;

                    var newAudiences = string.Join("\n", newLoyalties.Select((l, i) => l >= 0 ? audiences[i].Name : "").Where(s => s.Count() != 0).Select(Visuals.Positive));
                    var losingAudiences = string.Join("\n", newLoyalties.Select((l, i) => (l < 0 && GetUsers(product, i) > 0 && !IsAimingForSpecificAudience(product, i)) ? audiences[i].Name : "").Where(s => s.Count() != 0).Select(Visuals.Negative));

                    var losingAudiencesMsg = losingAudiences.Count() > 0 ? "\nBut you will lose\n\n" + losingAudiences : "";

                    NotificationUtils.AddSimplePopup(gameContext, "Product positioning changed", 
                        "You will start getting\n\n" + newAudiences + losingAudiencesMsg);
                }
            }
            else
            {
                if (Companies.IsDirectCompetitor(playerFlagship, product))
                {
                    NotificationUtils.AddSimplePopup(gameContext, Visuals.Negative($"New competitor!"), $"{product.company.Name} will compete with {playerFlagship.company.Name}");
                }
            }
        }

        public static bool IsFocusingOneAudience(GameEntity product)
        {
            var audiences = GetAudienceInfos();

            var positioning = product.productPositioning.Positioning;
            bool isFocusingOneAudience = positioning < audiences.Count;

            return isFocusingOneAudience;
        }

        public static bool IsFocusingMoreThanOneAudience(GameEntity product)
        {
            return !IsFocusingOneAudience(product);
        }
    }
}

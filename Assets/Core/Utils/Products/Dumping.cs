﻿namespace Assets.Core
{
    public static partial class Products
    {
        public static void ToggleDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = !product.isDumping;
        }

        public static void StartDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = true;
        }

        public static void StopDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = false;
        }

        // --------------------------------

        public static bool IsUpgradeEnabled(GameEntity product, ProductUpgrade productUpgrade)
        {
            var u = product.productUpgrades.upgrades;

            return u.ContainsKey(productUpgrade) && u[productUpgrade];
        }

        public static void SetUpgrade(GameEntity product, ProductUpgrade productUpgrade, GameContext gameContext, bool state)
        {
            product.productUpgrades.upgrades[productUpgrade] = state;

            ScaleTeam(product, gameContext);
        }
    }
}

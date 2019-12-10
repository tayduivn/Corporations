﻿namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static float GetMarketStatePriceModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:    return 1;
                case NicheLifecyclePhase.Trending:      return 0.8f;
                case NicheLifecyclePhase.MassGrowth:       return 0.7f;
                case NicheLifecyclePhase.Decay:         return 0.6f;

                default: return 0;
            }
        }

        public static float GetMarketStateAdCostModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:    return 0.1f;
                case NicheLifecyclePhase.Trending:      return 0.5f;
                case NicheLifecyclePhase.MassGrowth:       return 1f;
                case NicheLifecyclePhase.Decay:         return 2f;

                default: return 0;
            }
        }
    }
}

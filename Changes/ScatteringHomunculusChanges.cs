using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class ScatteringHomunculusChanges
    {
        public static void Init()
        {
            var sonicSuffering = LoadedAssetsHandler.GetEnemyAbility("SonicSuffering_A");
            sonicSuffering.intents = new()
            {
                TargetIntent(Targeting.Unit_AllAllies, IntentForHealing(2)),
                TargetIntent(Targeting.Unit_AllOpponents, IntentForHealing(2)),
            };
        }
    }
}

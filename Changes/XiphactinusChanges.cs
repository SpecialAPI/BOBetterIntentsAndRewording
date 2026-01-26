using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class XiphactinusChanges
    {
        public static void Init()
        {
            var persistence = LoadedAssetsHandler.GetEnemyAbility("PersistenceOfTime_A");
            persistence.intents = new()
            {
                TargetIntent(Targeting.Unit_AllAllies, IntentForDamage(1)),
                TargetIntent(Targeting.Unit_AllOpponents, IntentForDamage(1)),
            };
        }
    }
}

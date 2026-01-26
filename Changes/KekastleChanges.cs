using BrutalAPI;
using Grimoire.Content.Intent;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class KekastleChanges
    {
        public static void Init()
        {
            var wrigglingWrath = LoadedAssetsHandler.GetEnemyAbility("WrigglingWrath_A");
            wrigglingWrath.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfAll, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_MaxHealth_Alt.ToString(), IntentType_GameIDs.Status_Frail.ToString())
            };

            var revoltingRevolution = LoadedAssetsHandler.GetEnemyAbility("RevoltingRevolution_A");
            revoltingRevolution.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfAll, PassiveIntents.PA_Infestation),
                TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Status_OilSlicked.ToString()),
                TargetIntent(Targeting.Slot_SelfAll, IntentType_GameIDs.Mana_Consume.ToString()),
            };
        }
    }
}

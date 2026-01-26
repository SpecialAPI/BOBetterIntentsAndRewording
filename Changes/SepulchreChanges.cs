using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class SepulchreChanges
    {
        public static void Init()
        {
            var chastity = LoadedAssetsHandler.GetEnemyAbility("Repent_A");
            chastity.intents = new()
            {
                TargetIntent(Targeting.Unit_OtherAllies, IntentType_GameIDs.Damage_Death.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentForHealing(200), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_MaxHealth_Alt.ToString()),
            };
        }
    }
}

using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class SuckleAndGulperChanges
    {
        public static void Init()
        {
            var suckler = LoadedAssetsHandler.GetEnemyAbility("Suckle_A");
            suckler.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentForHealing(1), IntentType_GameIDs.Misc_Currency.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString()),
            };

            var bloodBlisteringSuckle = LoadedAssetsHandler.GetEnemyAbility("BloodBlister_A");
            bloodBlisteringSuckle.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentForHealing(1), IntentType_GameIDs.Misc_Currency.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString()),
            };

            var gildedGulp = LoadedAssetsHandler.GetEnemyAbility("GildedGulp_A");
            gildedGulp.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Misc.ToString(), IntentType_GameIDs.Misc_Currency.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString()),
            };
        }
    }
}

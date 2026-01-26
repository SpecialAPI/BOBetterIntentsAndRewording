using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class ChoirBoyChanges
    {
        public static void Init()
        {
            var ringABell = LoadedAssetsHandler.GetEnemyAbility("RingABell_A");
            ringABell.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_DivineProtection.ToString(), IntentForDamage(10), IntentType_GameIDs.Mana_Modify.ToString())
            };

            var reverberation = LoadedAssetsHandler.GetEnemyAbility("RapturousReverberation_A");
            reverberation.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Mana_Consume.ToString(), IntentType_GameIDs.Mana_Modify.ToString(), IntentType_GameIDs.Status_Scars.ToString())
            };
        }
    }
}

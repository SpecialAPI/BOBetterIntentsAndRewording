using BOBetterIntentsAndRewording.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class SynodChanges
    {
        public static void Init()
        {
            var exhume = LoadedAssetsHandler.GetEnemyAbility("Exhume_A");
            exhume.intents = new()
            {
                TargetIntent(Targeting.Slot_AllyAllSlots.FilterByOccupied(false), IntentType_GameIDs.Misc_Hidden.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Status_Scars.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString())
            };
        }
    }
}

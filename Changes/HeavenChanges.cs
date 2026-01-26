using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class HeavenChanges
    {
        public static void Init()
        {
            var omnipresent = LoadedAssetsHandler.GetEnemyAbility("Omnipresent_A");
            omnipresent.intents = new()
            {
                TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Status_Scars.ToString()),
                TargetIntent(Targeting.Slot_SelfAll, IntentType_GameIDs.Misc_State_Stand.ToString()),
                TargetIntent(Targeting.GenerateGenericTarget([1], true), IntentType_GameIDs.Other_Spawn.ToString()),
                TargetIntent(Targeting.GenerateGenericTarget([3], true), IntentType_GameIDs.Other_Spawn.ToString()),
            };
        }
    }
}

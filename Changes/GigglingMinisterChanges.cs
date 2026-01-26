using BrutalAPI;
using Grimoire.Content.Intent;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class GigglingMinisterChanges
    {
        public static void Init()
        {
            var mindgames = LoadedAssetsHandler.GetEnemyAbility("MindGames_A");
            mindgames.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Damage_Death.ToString()),
                TargetIntent(Targeting.Slot_OpponentSides, IntentForDamage(10)),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Misc_Hidden.ToString()),
            };

            var marrowToucher = LoadedAssetsHandler.GetEnemyAbility("MarrowToucher_A");
            marrowToucher.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_Ruptured.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Swap_Sides.ToString()),
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_Ruptured.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Swap_Sides.ToString()),
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_Ruptured.ToString()),
            };

            var traumaBond = LoadedAssetsHandler.GetEnemyAbility("TraumaBond_A");
            traumaBond.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfSlot, PassiveIntents.PA_Fleeting_Remove)
            };
        }
    }
}

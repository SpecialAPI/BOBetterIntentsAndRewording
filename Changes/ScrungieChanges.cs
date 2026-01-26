using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class ScrungieChanges
    {
        public static void Init()
        {
            var talons = LoadedAssetsHandler.GetEnemyAbility("Talons_A");
            talons.intents = new()
            {
                TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Misc_Hidden.ToString(), IntentForDamage(6))
            };

            var chickenHawk = LoadedAssetsHandler.GetEnemyAbility("ChickenHawk_A");
            chickenHawk.intents = new()
            {
                TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Misc_State_Sit.ToString()),
                TargetIntent(Targeting.Spec_Unit_AllOpponents_Weakest, IntentForDamage(5)),
            };

            var deathFromAbove = LoadedAssetsHandler.GetEnemyAbility("DeathFromAbove_A");
            deathFromAbove.intents = new()
            {
                TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Misc_State_Stand.ToString()),
                TargetIntent(Targeting.Spec_Unit_AllOpponents_Strongest, IntentForDamage(5)),
            };
        }
    }
}

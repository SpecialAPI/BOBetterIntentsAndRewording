using BOBetterIntentsAndRewording.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class FoundlingChanges
    {
        public static void Init()
        {
            var organFailure = LoadedAssetsHandler.GetEnemyAbility("OrganFailure_A");
            organFailure.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Misc_State_Sit.ToString(), IntentForDamage(99))
            };

            var deathKnell = LoadedAssetsHandler.GetEnemyAbility("DeathKnell_A");
            deathKnell.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Misc_State_Stand.ToString()),
                TargetIntent(Targeting.Slot_Front, IntentForDamage(99))
            };

            var primitiveTerror = LoadedAssetsHandler.GetEnemyAbility("PrimitiveTerror_A");
            primitiveTerror.intents = new()
            {
                TargetIntent(Targeting.Spec_Unit_AllOpponents_Strongest, PassiveIntents.PA_Fleeting)
            };

            var bubonicRhapsody = LoadedAssetsHandler.GetEnemyAbility("BubonicRhapsody_A");
            var bubonicRhapsodyCascade = bubonicRhapsody.effects.FirstOrDefault(x => x.effect is DamageOnDoubleCascadeEffect);
            bubonicRhapsody.intents = bubonicRhapsodyCascade.GenerateIntentsForDamageCascade();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class KekoChanges
    {
        public static void Init()
        {
            var mandibles = LoadedAssetsHandler.GetEnemyAbility("Mandibles_A");
            mandibles.intents = new()
            {
                TargetIntent(Targeting.Slot_OpponentLeft, IntentForDamage(1)),
                TargetIntent(Targeting.Slot_OpponentRight, IntentForDamage(1)),
            };
        }
    }
}

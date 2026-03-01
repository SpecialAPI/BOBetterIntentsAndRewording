using BOBetterIntentsAndRewording.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class LeviatChanges
    {
        public static void Init()
        {
            for(var i = 1; i <= 4; i++)
            {
                var disciplineSelfDmg = i switch
                {
                    1 => 2,
                    2 => 3,
                    3 => 3,
                    4 => 4,

                    _ => 0,
                };
                var discipline = LoadedAssetsHandler.GetCharacterAbility($"Discipline_{i}_A");
                var disciplineCascade = discipline.effects.FirstOrDefault(x => x.effect is DamageOnDoubleCascadeEffect);
                discipline.intents =
                [
                    ..disciplineCascade.GenerateIntentsForDamageCascade(),
                    TargetIntent(Targeting.Slot_SelfAndRight, IntentForDamage(disciplineSelfDmg))
                ];

                var unravelSelfDmg = 2;
                var unravel = LoadedAssetsHandler.GetCharacterAbility($"Unravel_{i}_A");
                var unravelCascade = unravel.effects.FirstOrDefault(x => x.effect is HealOnCascadeEffect);
                unravel.intents =
                [
                    TargetIntent(Targeting.Slot_SelfAndLeft, IntentForDamage(unravelSelfDmg)),
                    ..unravelCascade.GenerateIntentsForHealCascade()
                ];
            }
        }
    }
}

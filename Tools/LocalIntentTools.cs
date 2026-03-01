using BOBetterIntentsAndRewording.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BOBetterIntentsAndRewording.Tools
{
    public static class LocalIntentTools
    {
        private static readonly List<(int? minInclusive, int? maxInclusive, string intent)> damageIntentRanges = new()
        {
            (null,  0,      IntentType_GameIDs.Misc_Hidden.ToString()),
            (1,     2,      IntentType_GameIDs.Damage_1_2.ToString()),
            (3,     6,      IntentType_GameIDs.Damage_3_6.ToString()),
            (7,     10,     IntentType_GameIDs.Damage_7_10.ToString()),
            (11,    15,     IntentType_GameIDs.Damage_11_15.ToString()),
            (16,    20,     IntentType_GameIDs.Damage_16_20.ToString()),
            (21,    null,   IntentType_GameIDs.Damage_21.ToString()),
        };

        private static readonly List<(int? minInclusive, int? maxInclusive, string intent)> healIntentRanges = new()
        {
            (null,  0,      IntentType_GameIDs.Misc_Hidden.ToString()),
            (1,     4,      IntentType_GameIDs.Heal_1_4.ToString()),
            (5,     10,     IntentType_GameIDs.Heal_5_10.ToString()),
            (11,    20,     IntentType_GameIDs.Heal_11_20.ToString()),
            (21,    null,   IntentType_GameIDs.Heal_21.ToString()),
        };

        public static List<IntentTargetInfo> GenerateIntentsForDamageCascade(this EffectInfo effect)
        {
            if (effect == null)
                return [];

            if (effect.effect is not DamageOnDoubleCascadeEffect cascade)
                return [];

            if(effect.targets == null)
                return [];

            var intents = new List<IntentTargetInfo>();
            var usedIntents = new List<string>();
            var reach = effect.targets.GetMaxDamageCascadeReach();
            var damageAmount = effect.entryVariable;

            for(var i = 0; i < reach; i++)
            {
                if (damageIntentRanges.TryGetFromRange(damageAmount, out var info) && !usedIntents.Contains(info.item))
                {
                    var target = ScriptableObject.CreateInstance<DamageCascadeIntentTargeting>();
                    target.orig = effect.targets;

                    target.minDamageInclusive = info.minInclusive;
                    target.maxDamageInclusive = info.maxInclusive;

                    target.origDamage = effect.entryVariable;
                    target.applyDecreaseOnlyIfDamageOver0 = cascade._ApplyDecreaseOnlyIfDamageOver0;
                    target.cascadeDecrease = cascade._cascadeDecrease;
                    target.consistentCascade = cascade._consistentCascade;
                    target.decreaseAsPercentage = cascade._decreaseAsPercentage;

                    intents.Add(new()
                    {
                        targets = target,
                        intents = [info.item]
                    });
                    usedIntents.Add(info.item);
                }

                if(!cascade._ApplyDecreaseOnlyIfDamageOver0 || damageAmount > 0)
                {
                    if (cascade._decreaseAsPercentage)
                    {
                        var f = (float)cascade._cascadeDecrease * damageAmount / 100f;
                        damageAmount = Mathf.Max(0, Mathf.FloorToInt(f));
                    }
                    else
                        damageAmount -= cascade._cascadeDecrease;
                }
            }

            return intents;
        }

        private static int GetMaxDamageCascadeReach(this BaseCombatTargettingSO target)
        {
            const int MAX = 5;

            if(target == null)
                return 0;

            if(target is Targetting_BySlot_Index simpleRelative)
            {
                if (simpleRelative.slotPointerDirections.Length == 0)
                    return 0;

                var firstTargetOffset = simpleRelative.slotPointerDirections[0];

                var leftReach = simpleRelative.slotPointerDirections.Count(x => x < firstTargetOffset);
                var rightReach = simpleRelative.slotPointerDirections.Count(x => x > firstTargetOffset);

                return Mathf.Min(MAX, 1 + Mathf.Max(leftReach, rightReach));
            }
            else if (target is GenericTargetting_BySlot_Index absolute)
            {
                if (absolute.slotPointerDirections.Length == 0)
                    return 0;

                var firstTargetOffset = absolute.slotPointerDirections[0];

                var leftReach = absolute.slotPointerDirections.Count(x => x < firstTargetOffset);
                var rightReach = absolute.slotPointerDirections.Count(x => x > firstTargetOffset);

                return Mathf.Min(MAX, 1 + Mathf.Max(leftReach, rightReach));
            }
            else if(target is CustomOpponentTargetting_BySlot_Index bigEnemyRelative)
            {
                if(bigEnemyRelative._slotPointerDirections.Length == 0)
                    return 0;

                var firstTargetOffset = bigEnemyRelative._slotPointerDirections[0];

                var leftReach = bigEnemyRelative._slotPointerDirections.Count(x => x < firstTargetOffset);
                var rightReach = bigEnemyRelative._slotPointerDirections.Count(x => x > firstTargetOffset);

                return Mathf.Min(MAX, 1 + Mathf.Max(leftReach, rightReach));
            }

            return MAX;
        }

        public static List<IntentTargetInfo> GenerateIntentsForHealCascade(this EffectInfo effect)
        {
            if (effect == null)
                return [];

            if (effect.effect is not HealOnCascadeEffect cascade)
                return [];

            if (effect.targets == null)
                return [];

            var intents = new List<IntentTargetInfo>();
            var usedIntents = new List<string>();
            var reach = effect.targets.GetMaxHealCascadeReach();
            var healAmount = effect.entryVariable;

            for (var i = 0; i < reach; i++)
            {
                if(healIntentRanges.TryGetFromRange(healAmount, out var info) && !usedIntents.Contains(info.item))
                {
                    var target = ScriptableObject.CreateInstance<HealCascadeIntentTargeting>();
                    target.orig = effect.targets;

                    target.minHealInclusive = info.minInclusive;
                    target.maxHealInclusive = info.maxInclusive;

                    target.origHeal = effect.entryVariable;
                    target.cascadeDecrease = cascade._cascadeDecrease;
                    target.consistentCascade = cascade._consistentCascade;

                    intents.Add(new()
                    {
                        targets = target,
                        intents = [info.item]
                    });
                    usedIntents.Add(info.item);
                }

                healAmount -= cascade._cascadeDecrease;
            }

            return intents;
        }

        private static int GetMaxHealCascadeReach(this BaseCombatTargettingSO target)
        {
            const int MAX = 5;

            if (target == null)
                return 0;

            if (target is Targetting_BySlot_Index simpleRelative)
                return Mathf.Min(MAX, simpleRelative.slotPointerDirections.Length);
            else if (target is GenericTargetting_BySlot_Index absolute)
                return Mathf.Min(MAX, absolute.slotPointerDirections.Length);
            else if (target is CustomOpponentTargetting_BySlot_Index bigEnemyRelative)
                return Mathf.Min(MAX, bigEnemyRelative._slotPointerDirections.Length);

            return MAX;
        }

        private static bool TryGetFromRange<T>(this IEnumerable<(int?, int?, T)> rangeList, int value, out (int? minInclusive, int? maxInclusive, T item) res)
        {
            foreach(var (minInclusive, maxInclusive, item) in rangeList)
            {
                if (minInclusive is int min && value < min)
                    continue;

                if (maxInclusive is int max && value > max)
                    continue;

                res = (minInclusive, maxInclusive, item);
                return true;
            }

            res = default;
            return false;
        }
    }
}

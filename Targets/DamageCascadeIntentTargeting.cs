using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BOBetterIntentsAndRewording.Targets
{
    public class DamageCascadeIntentTargeting : BaseCombatTargettingSO
    {
        public BaseCombatTargettingSO orig;

        public int? minDamageInclusive;
        public int? maxDamageInclusive;

        public int origDamage;
        public bool applyDecreaseOnlyIfDamageOver0;
        public int cascadeDecrease;
        public bool consistentCascade;
        public bool decreaseAsPercentage;

        public bool alwaysAddFirstTarget = true;
        public bool stopWhenAt0;

        public override bool AreTargetAllies => orig.AreTargetAllies;
        public override bool AreTargetSlots => orig.AreTargetSlots;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            var origTargets = orig.GetTargets(slots, casterSlotID, isCasterCharacter);
            if (origTargets.Length == 0)
                return [];

            var firstTarget = origTargets[0];
            var targets = new List<TargetSlotInfo>();

            var damageAmount = origDamage;
            if (IsDamageInRange(damageAmount) && (alwaysAddFirstTarget || firstTarget.HasUnit))
                targets.Add(firstTarget);

            if(!applyDecreaseOnlyIfDamageOver0 || damageAmount > 0)
                damageAmount = CalculateDecrease(damageAmount);

            if ((damageAmount <= 0 && stopWhenAt0) || !firstTarget.HasUnit)
                return [..targets];

            var leftGuys = new List<TargetSlotInfo>();
            var rightGuys = new List<TargetSlotInfo>();
            foreach(var t in origTargets)
            {
                if (t.Unit == firstTarget.Unit)
                    continue;

                if(t.SlotID < firstTarget.SlotID)
                    leftGuys.Add(t);
                if(t.SlotID > firstTarget.SlotID)
                    rightGuys.Add(t);
            }

            var cascadeLeft = true;
            var cascadeRight = true;
            for(var i = 0; (i < leftGuys.Count) || (i < rightGuys.Count); i++)
            {
                if (cascadeLeft)
                {
                    if(i >= leftGuys.Count || !leftGuys[i].HasUnit)
                    {
                        if(consistentCascade)
                            cascadeLeft = false;
                    }
                    else
                    {
                        if (IsDamageInRange(damageAmount))
                            targets.Add(leftGuys[i]);
                    }
                }
                if (cascadeRight)
                {
                    if (i >= rightGuys.Count || !rightGuys[i].HasUnit)
                    {
                        if (consistentCascade)
                            cascadeRight = false;
                    }
                    else
                    {
                        if (IsDamageInRange(damageAmount))
                            targets.Add(rightGuys[i]);
                    }
                }

                if (!applyDecreaseOnlyIfDamageOver0 || damageAmount > 0)
                    damageAmount = CalculateDecrease(damageAmount);

                if (damageAmount <= 0 && stopWhenAt0)
                    break;
            }

            return [..targets];
        }

        public bool IsDamageInRange(int v)
        {
            if (minDamageInclusive is int min && v < min)
                return false;

            if(maxDamageInclusive is int max && v > max)
                return false;

            return true;
        }

        public int CalculateDecrease(int current)
        {
            if (decreaseAsPercentage)
            {
                var f = (float)cascadeDecrease * current / 100f;
                return Mathf.Max(0, Mathf.FloorToInt(f));
            }

            return current - cascadeDecrease;
        }
    }
}

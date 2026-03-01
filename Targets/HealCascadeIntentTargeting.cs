using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Targets
{
    public class HealCascadeIntentTargeting : BaseCombatTargettingSO
    {
        public BaseCombatTargettingSO orig;

        public int? minHealInclusive;
        public int? maxHealInclusive;

        public int origHeal;
        public int cascadeDecrease;
        public bool consistentCascade;

        public bool alwaysAddFirstTarget = true;
        public bool stopWhenAt0;

        public override bool AreTargetAllies => orig.AreTargetAllies;
        public override bool AreTargetSlots => orig.AreTargetSlots;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            var origTargets = orig.GetTargets(slots, casterSlotID, isCasterCharacter);
            var targets = new List<TargetSlotInfo>();

            var healAmount = origHeal;
            for(var i = 0; i < origTargets.Length; i++)
            {
                var t = origTargets[i];

                if (!t.HasUnit)
                {
                    if(alwaysAddFirstTarget && i == 0 & IsHealInRange(healAmount))
                        targets.Add(t);

                    if (consistentCascade)
                        break;

                    continue;
                }

                if(IsHealInRange(healAmount))
                    targets.Add(t);

                healAmount -= cascadeDecrease;
            }

            return [..targets];
        }

        public bool IsHealInRange(int v)
        {
            if (minHealInclusive is int min && v < min)
                return false;

            if (maxHealInclusive is int max && v > max)
                return false;

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Targets
{
    public abstract class FilterTargetingBase : BaseCombatTargettingSO
    {
        public BaseCombatTargettingSO orig;

        public override bool AreTargetAllies => orig.AreTargetAllies;
        public override bool AreTargetSlots => orig.AreTargetSlots;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            var origTargets = orig.GetTargets(slots, casterSlotID, isCasterCharacter);
            var targets = new List<TargetSlotInfo>();

            foreach (var target in origTargets)
            {
                if (!ShouldIncludeTarget(target, slots, casterSlotID, isCasterCharacter))
                    continue;

                targets.Add(target);
            }

            return [..targets];
        }

        public abstract bool ShouldIncludeTarget(TargetSlotInfo target, SlotsCombat slots, int casterSlotID, bool isCasterCharacter);
    }
}

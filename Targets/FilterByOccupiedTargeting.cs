using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Targets
{
    public class FilterByOccupiedTargeting : FilterTargetingBase
    {
        public bool targetsShouldBeOccupied;

        public override bool ShouldIncludeTarget(TargetSlotInfo target, SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            return target.HasUnit == targetsShouldBeOccupied;
        }
    }
}

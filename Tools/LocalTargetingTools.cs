using BOBetterIntentsAndRewording.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BOBetterIntentsAndRewording.Tools
{
    public static class LocalTargetingTools
    {
        public static BaseCombatTargettingSO FilterByOccupied(this BaseCombatTargettingSO orig, bool targetsShouldBeOccupied)
        {
            var t = ScriptableObject.CreateInstance<FilterByOccupiedTargeting>();
            t.orig = orig;
            t.targetsShouldBeOccupied = targetsShouldBeOccupied;

            return t;
        }
    }
}

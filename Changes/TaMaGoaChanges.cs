using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class TaMaGoaChanges
    {
        public static void Init()
        {
            var born = LoadedAssetsHandler.GetEnemyAbility("HisChildIsBorn_A");
            born.intents = new()
            {
                TargetIntent(Targeting.Generic_Ally_Middle, IntentType_GameIDs.Damage_Death.ToString(), IntentType_GameIDs.Mana_Consume.ToString(), IntentType_GameIDs.Other_Spawn.ToString())
            };
        }
    }
}

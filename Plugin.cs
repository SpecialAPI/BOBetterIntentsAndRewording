using BepInEx;
using BrutalAPI;
using Grimoire.Content.Intent;
using HarmonyLib;
using System;

using static Pentacle.Tools.IntentTools;

namespace BOBetterIntentsAndRewording
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string MOD_GUID = "SpecialAPI.BetterIntentsAndRewording";
        public const string MOD_NAME = "Better Intents and Rewording";
        public const string MOD_VERSION = "1.0.0";

        public void Awake()
        {
            var harmony = new Harmony(MOD_GUID);
            harmony.PatchAll();

            var english = LanguageChanges.languageChanges.GetOrAdd("en_US");
            english.SetPassiveDescriptions("BronzosBlessing_PA",
                "This party member can no longer be damaged or instantly killed. Lose an equivalent amount of coins upon blocking damage. Always lose 3 coins for overflow and wrong pigment damage. Lose 50 coins upon blocking an instant death.\nUpon losing all money, combat ends.",
                "This enemy can no longer be damaged or instantly killed. Lose an equivalent amount of coins upon blocking damage. Always lose 3 coins for overflow and wrong pigment damage. Lose 50 coins upon blocking an instant death.\nUpon losing all money, combat ends.");
            english.SetPassiveEnemyDescription("Forgetful_PA",
                "Upon performing an ability, this enemy will forget that ability. If, after that, all abilities are forgotten, this enemy will unforget all abilities other than the one that was performed.\nForgotten abilities cannot be used.");

            var bankrupt = LoadedAssetsHandler.GetEnemyAbility("Bankrupt_A");
            bankrupt.intents = new()
            {
                TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Damage_Death.ToString())
            };

            var donation = LoadedAssetsHandler.GetEnemyAbility("CharitableDonation_A");
            donation.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentForDamage(7)),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Swap_Sides.ToString())
            };
            english.SetAbilityDescription(donation, "Deals 7 damage to the Opposing party member. 50% chance to move Bronzo to the Left or Right.");

            var pennyPincher = LoadedAssetsHandler.GetEnemyAbility("PennyPincher_A");
            pennyPincher.intents = new()
            {
                TargetIntent(Targeting.Slot_OpponentSides, IntentType_GameIDs.Field_Constricted.ToString(), IntentForDamage(2)),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Swap_Sides.ToString())
            };
            english.SetAbilityDescription(pennyPincher, "Inflicts 1 Constricted to the Left and Right Opposing positions. Deals 2 damage to the Left and Right party members. Moves Bronzo to the Left or Right.");

            var dividents = LoadedAssetsHandler.GetEnemyAbility("Dividends_A");
            dividents.intents = new()
            {
                TargetIntent(Targeting.BigEnemy_Front_Offset_0, IntentForDamage(7))
            };
            english.SetAbilityDescription(dividents, "Deals 7 damage to the Left Opposing party member.");

            var residuals = LoadedAssetsHandler.GetEnemyAbility("Residuals_A");
            residuals.intents = new()
            {
                TargetIntent(Targeting.BigEnemy_Front_Offset_1, IntentForDamage(7))
            };
            english.SetAbilityDescription(residuals, "Deals 7 damage to the Right Opposing party member.");

            var highInterest = LoadedAssetsHandler.GetEnemyAbility("HighInterest_A");
            highInterest.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentForDamage(50))
            };
            english.SetAbilityDescription(highInterest, "Bronzo deals an amount of damage equal to half of your current currency to the Opposing party member.");

            var mindgames = LoadedAssetsHandler.GetEnemyAbility("MindGames_A");
            mindgames.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Damage_Death.ToString()),
                TargetIntent(Targeting.Slot_OpponentSides, IntentForDamage(10)),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Misc_Hidden.ToString()),
            };

            var marrowToucher = LoadedAssetsHandler.GetEnemyAbility("MarrowToucher_A");
            marrowToucher.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_Ruptured.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Swap_Sides.ToString()),
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_Ruptured.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Swap_Sides.ToString()),
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_Ruptured.ToString()),
            };

            var traumaBond = LoadedAssetsHandler.GetEnemyAbility("TraumaBond_A");
            traumaBond.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfSlot, PassiveIntents.PA_Fleeting_Remove)
            };

            var wrigglingWrath = LoadedAssetsHandler.GetEnemyAbility("WrigglingWrath_A");
            wrigglingWrath.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfAll, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_MaxHealth_Alt.ToString(), IntentType_GameIDs.Status_Frail.ToString())
            };

            var revoltingRevolution = LoadedAssetsHandler.GetEnemyAbility("RevoltingRevolution_A");
            revoltingRevolution.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfAll, PassiveIntents.PA_Infestation),
                TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Status_OilSlicked.ToString()),
                TargetIntent(Targeting.Slot_SelfAll, IntentType_GameIDs.Mana_Consume.ToString()),
            };

            var born = LoadedAssetsHandler.GetEnemyAbility("HisChildIsBorn_A");
            born.intents = new()
            {
                TargetIntent(Targeting.Generic_Ally_Middle, IntentType_GameIDs.Damage_Death.ToString(), IntentType_GameIDs.Mana_Consume.ToString(), IntentType_GameIDs.Other_Spawn.ToString())
            };

            var suckler = LoadedAssetsHandler.GetEnemyAbility("Suckle_A");
            suckler.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentForHealing(1), IntentType_GameIDs.Misc_Currency.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString()),
            };

            var bloodBlisteringSuckle = LoadedAssetsHandler.GetEnemyAbility("BloodBlister_A");
            bloodBlisteringSuckle.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentForHealing(1), IntentType_GameIDs.Misc_Currency.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString()),
            };

            var gildedGulp = LoadedAssetsHandler.GetEnemyAbility("GildedGulp_A");
            gildedGulp.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Misc.ToString(), IntentType_GameIDs.Misc_Currency.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString()),
            };

            var ringABell = LoadedAssetsHandler.GetEnemyAbility("RingABell_A");
            ringABell.intents = new()
            {
                TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_DivineProtection.ToString(), IntentForDamage(10), IntentType_GameIDs.Mana_Modify.ToString())
            };

            var reverberation = LoadedAssetsHandler.GetEnemyAbility("RapturousReverberation_A");
            reverberation.intents = new()
            {
                TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Mana_Consume.ToString(), IntentType_GameIDs.Mana_Modify.ToString(), IntentType_GameIDs.Status_Scars.ToString())
            };

            var sonicSuffering = LoadedAssetsHandler.GetEnemyAbility("SonicSuffering_A");
            sonicSuffering.intents = new()
            {
                TargetIntent(Targeting.Unit_AllAllies, IntentForHealing(2)),
                TargetIntent(Targeting.Unit_AllOpponents, IntentForHealing(2)),
            };

            var chastity = LoadedAssetsHandler.GetEnemyAbility("Repent_A");
            chastity.intents = new()
            {
                TargetIntent(Targeting.Unit_OtherAllies, IntentType_GameIDs.Damage_Death.ToString()),
                TargetIntent(Targeting.Slot_SelfSlot, IntentForHealing(200), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_Spawn.ToString(), IntentType_GameIDs.Other_MaxHealth_Alt.ToString()),
            };

            var persistence = LoadedAssetsHandler.GetEnemyAbility("PersistenceOfTime_A");
            persistence.intents = new()
            {
                TargetIntent(Targeting.Unit_AllAllies, IntentForDamage(1)),
                TargetIntent(Targeting.Unit_AllOpponents, IntentForDamage(1)),
            };
        }
    }
}

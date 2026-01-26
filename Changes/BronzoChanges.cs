using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording.Changes
{
    public static class BronzoChanges
    {
        public static void Init()
        {
            var english = LanguageChanges.English;

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
        }
    }
}

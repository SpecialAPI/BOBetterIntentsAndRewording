using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace BOBetterIntentsAndRewording
{
    [HarmonyPatch]
    public static class LanguageChanges
    {
        public static readonly Dictionary<string, LanguageChangeData> languageChanges = [];

        public static LanguageChangeData English => languageChanges.GetOrAdd("en_US");

        [HarmonyPatch(typeof(InGameLanguage), nameof(InGameLanguage.InitializeLocalisation))]
        [HarmonyPostfix]
        public static void ApplyLanguageChanges_Postfix(InGameLanguage __instance, LocalisationData data)
        {
            if (!languageChanges.TryGetValue(data.localisationID, out var change))
                return;

            static void ApplyChangesToPoolS(Dictionary<string, string> pool, Dictionary<string, string> changes)
            {
                foreach(var kvp in changes)
                    pool[kvp.Key] = kvp.Value;
            }
            static void ApplyChangesToPoolLS(Dictionary<string, List<string>> pool, Dictionary<string, List<string>> changes)
            {
                foreach (var kvp in changes)
                    pool[kvp.Key] = kvp.Value;
            }
            static void ApplyChangesToPoolSP(Dictionary<string, StringPairData> pool, Dictionary<string, StringPairData> changes)
            {
                foreach (var kvp in changes)
                {
                    var pair = kvp.Value;

                    if (pair.text != null && pair.description != null)
                        pool[kvp.Key] = kvp.Value;
                    else if (pool.TryGetValue(kvp.Key, out var existing))
                        pool[kvp.Key] = new(pair.text ?? existing.text, pair.description ?? existing.description);
                }
            }
            static void ApplyChangesToPoolST(Dictionary<string, StringTrioData> pool, Dictionary<string, StringTrioData> changes)
            {
                foreach (var kvp in changes)
                {
                    var trio = kvp.Value;

                    if (trio.text != null && trio.description != null && trio.subDescription != null)
                        pool[kvp.Key] = kvp.Value;
                    else if(pool.TryGetValue(kvp.Key, out var existing))
                        pool[kvp.Key] = new(trio.text ?? existing.text, trio.description ?? existing.description, trio.subDescription ?? existing.subDescription);
                }
            }

            ApplyChangesToPoolS(__instance._uiData,                  change.uiData);
            ApplyChangesToPoolS(__instance._characterData,           change.characterData);
            ApplyChangesToPoolS(__instance._enemyData,               change.enemyData);
            ApplyChangesToPoolLS(__instance._cinematicData,           change.cinematicData);
            ApplyChangesToPoolLS(__instance._gameOverData,            change.gameOverData);
            ApplyChangesToPoolSP(__instance._characterAbilityData,    change.abilityData);
            ApplyChangesToPoolSP(__instance._statusData,              change.statusData);
            ApplyChangesToPoolSP(__instance._glossaryData,            change.glossaryData);
            ApplyChangesToPoolST(__instance._passivesData,            change.passivesData);
            ApplyChangesToPoolST(__instance._itemsData,               change.itemsData);
            ApplyChangesToPoolST(__instance._achievementsData,        change.achievementsData);
        }
    }

    public class LanguageChangeData
    {
        public Dictionary<string, string> uiData                        = [];
        public Dictionary<string, string> characterData                 = [];
        public Dictionary<string, string> enemyData                     = [];
        public Dictionary<string, List<string>> cinematicData           = [];
        public Dictionary<string, List<string>> gameOverData            = [];
        public Dictionary<string, StringPairData> abilityData           = [];
        public Dictionary<string, StringPairData> statusData            = [];
        public Dictionary<string, StringPairData> glossaryData          = [];
        public Dictionary<string, StringTrioData> passivesData          = [];
        public Dictionary<string, StringTrioData> itemsData             = [];
        public Dictionary<string, StringTrioData> achievementsData      = [];

        public void SetAbilityDescription(AbilitySO ab, string description)
        {
            if (!abilityData.TryGetValue(ab.name, out var pair))
                abilityData[ab.name] = pair = new(null, null);

            pair.description = description;
        }

        public void SetAbilityName(AbilitySO ab, string name)
        {
            if (!abilityData.TryGetValue(ab.name, out var pair))
                abilityData[ab.name] = pair = new(null, null);

            pair.text = name;
        }

        public void SetAbilityFullInfo(AbilitySO ab, string name, string description)
        {
            if (!abilityData.TryGetValue(ab.name, out var pair))
                abilityData[ab.name] = pair = new(null, null);

            pair.text = name;
            pair.description = description;
        }

        public void SetPassiveDescriptions(string passiveID, string chDescription, string enDescription)
        {
            if (!passivesData.TryGetValue(passiveID, out var trio))
                passivesData[passiveID] = trio = new(null, null, null);

            trio.description = chDescription;
            trio.subDescription = enDescription;
        }

        public void SetPassiveEnemyDescription(string passiveID, string enDescription)
        {
            if (!passivesData.TryGetValue(passiveID, out var trio))
                passivesData[passiveID] = trio = new(null, null, null);

            trio.subDescription = enDescription;
        }
    }
}

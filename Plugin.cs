using BepInEx;
using BrutalAPI;
using Grimoire.Content.Intent;
using HarmonyLib;
using System;

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

            KekoChanges.Init();
            KekastleChanges.Init();
            TaMaGoaChanges.Init();

            ScrungieChanges.Init();
            SuckleAndGulperChanges.Init();

            ChoirBoyChanges.Init();
            GigglingMinisterChanges.Init();
            ScatteringHomunculusChanges.Init();
            SepulchreChanges.Init();
            FoundlingChanges.Init();

            BronzoChanges.Init();
        }
    }
}

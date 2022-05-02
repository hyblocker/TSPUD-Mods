using CoreMods;
using MelonLoader;
using ModThatLetsYouMod;
using UnityEngine;

[assembly: MelonGame("Crows Crows Crows", "The Stanley Parable: Ultra Deluxe")]
[assembly: MelonInfo(typeof(CoreModsMain), "Core Mods", "0.0.1", "Hyblocker", "https://github.com/hyblocker/TSPUD-Mods")]
[assembly: MelonAdditionalDependencies("ModThatLetsYouMod")] // This mod requires ModThatLetsYouMod to even function, since it uses it
namespace CoreMods
{
    public class CoreModsMain : MelonMod
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static CoreModsMain Instance;
        public static Preferences modPreferences;

        public override void OnApplicationStart()
        {
            Instance = this;
            Settings.Initialize(out modPreferences);

            // Test register a settings category


            // Test create a new control binding

        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            DebugOptionPatch();
        }

        // Patches the debug settings tab
        private void DebugOptionPatch()
        {
            GameObject.Find("GameMaster/_TSPUD_MENU(Clone)/Root/Settings/PageTitles (Toggles)/Debug (PageButton)")?.SetActive(modPreferences.DebugTab);
        }
    }
}

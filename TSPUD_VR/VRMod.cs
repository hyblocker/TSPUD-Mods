#if false
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSPUD_VR;
using TSPUD_VR.Patches;
using TSPUD_VR.Util;
using UnityEngine.XR;
using Valve.VR;

[assembly: MelonGame("Crows Crows Crows", "The Stanley Parable: Ultra Deluxe")]
[assembly: MelonInfo(typeof(VRMod), "VR Mod", "0.0.1", "Hyblocker", "https://github.com/hyblocker/TSPUD-Mods")]
namespace TSPUD_VR
{
    public class VRMod : MelonMod
    {
        private const string SettingsCategory = "VR Mod";
        private const string EnabledSetting = "Enabled";

        /// <summary>
        /// Singleton
        /// </summary>
        public static VRMod Instance { get; private set; }

        public override void OnApplicationStart()
        {
            Instance = this;

            // TODO: Try OpenXR ???
            // Idfk why OpenVR doesn't want to even init no matter what I do
            // I'm shooting in the dark here :(

            // Init OpenVR
            InitVR();

            var category = MelonPreferences.CreateCategory(SettingsCategory, SettingsCategory);
            // var entry = category.CreateEntry(EnabledSetting, true, "FP fix enabled");
            // entry.OnValueChanged += (_, value) =>
            // {
            //     OnModSettingsApplied(value);
            // };
            // 
            // OnModSettingsApplied(entry.Value);
            MelonLogger.Msg("Patching methods...");
            HarmonyInstance.PatchAll();

            // Fix stanley controller for VR
            StanleyControllerPatch.PatchController();
        }

        public override void OnUpdate()
        {
            VRInputHelper.Update();
        }

        public override void OnApplicationLateStart()
        {
            InitVR();
        }

        private static void InitVR()
        {
            MelonLogger.Msg("Initializing SteamVR...");
            // Force enable unity XR? idfk i hate this engine
            XRSettings.enabled = true;

            SteamVR_Actions.PreInitialize();
            SteamVR.Initialize(true);

            // MelonLogger.Msg();

            // Register the app with SteamVR
            MelonLogger.Msg("Registering game with SteamVR ...");
            ApplicationManifestUtil.UpdateManifest(MelonUtils.GetGameDataDirectory() + @"\StreamingAssets\SteamVR\tspud.vrmanifest",
                                                    "steam.app.1703340",
                                                    "https://steamcdn-a.akamaihd.net/steam/apps/1703340/header.jpg",
                                                    "The Stanley Parable: Ultra Deluxe VR",
                                                    "VR mod for The Stanley Parable: Ultra Deluxe",
                                                    steamBuild: SteamManager.Initialized,
                                                    steamAppId: 1703340);

            MelonLogger.Msg("Loading SteamVR Actions...");
            MelonLogger.Msg(OpenVR.Input == null);
            OpenVR.Input.SetActionManifestPath(MelonUtils.GetGameDataDirectory() + @"\StreamingAssets\SteamVR\actions.json");
            VRInputHelper.Init();
        }
    }
}

#endif
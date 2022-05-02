using MelonLoader;
using ModThatLetsYouMod.CommonInjections;

namespace ModThatLetsYouMod
{
    /// <summary>
    /// This class initializes the modding framework
    /// </summary>
    internal class Bootstrap : MelonMod
    {
        private static Bootstrap Instance;
        public static Preferences config;

        public override void OnApplicationStart()
        {
            Instance = this;
            Settings.Initialize(out config);
            ModConsole.Log($"Starting ModThatLetsYouMod...", LogLevel.Normal);
            Hooking.SetHarmony(HarmonyInstance);
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            ModConsole.Log($"Loaded scene ID: {buildIndex} , Name: {sceneName}", LogLevel.Verbose);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            ModConsole.Log($"Initialized scene ID: {buildIndex} , Name: {sceneName}", LogLevel.Verbose);

            // This isn't in startup as we have to wait a bit until the settings page is available to us
            SettingsHooking.RegisterModsTab();
        }

        public override void OnUpdate()
        {

        }

        public override void OnLateUpdate()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnApplicationQuit()
        {

        }
    }
}

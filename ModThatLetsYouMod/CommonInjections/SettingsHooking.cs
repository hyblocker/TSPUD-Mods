using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModThatLetsYouMod.CommonInjections
{
    public static class SettingsHooking
    {
        private static GameObject settingsMenuTabContainer;
        private static GameObject ModsTabPageItem;
        private static Transform OriginalTabPageContainerRoot;
        private static Transform ModsTabPageContainerRoot;
        private static Transform ModsTabPageContainer;

        private static string PageTitlesStr = "PageTitles (Toggles)";
        private static string ModsButtonStr = "Mods (PageButton)";
        private static string ModsPageStr = "Settings_Mods";

        // Template controls
        private static GameObject ToggleControl;
        private static GameObject SliderControl;
        private static GameObject ButtonControl;
        private static GameObject KeybindControl;

        private static void TryFetchModsPage()
        {
            var settingsRootContainer = settingsMenuTabContainer.transform.parent;

            if (ModsTabPageContainerRoot == null)
            {
                // Try finding the mods page
                ModsTabPageContainerRoot = settingsRootContainer.Find(ModsPageStr);
                if (ModsTabPageContainerRoot == null) // If it's null, try creating it
                {
                    var GeneralTabPageContainer = ModsTabPageContainerRoot = settingsRootContainer.Find("Settings_General");
                    OriginalTabPageContainerRoot = GeneralTabPageContainer;
                    if (OriginalTabPageContainerRoot != null)
                    {
                        ModsTabPageContainerRoot = GameObject.Instantiate(OriginalTabPageContainerRoot.gameObject).transform;

                        // Re-parent it, and re-name too
                        ModsTabPageContainerRoot.parent = OriginalTabPageContainerRoot.transform.parent;
                        ModsTabPageContainer.SetSiblingIndex(OriginalTabPageContainerRoot.GetSiblingIndex() + 5); // 5th is mods
                        ModsTabPageContainerRoot.name = ModsPageStr;
                        ModsTabPageContainerRoot.localScale = Vector3.one;
                        ModsTabPageContainerRoot.position = OriginalTabPageContainerRoot.transform.position;
                        ModsTabPageContainerRoot.gameObject.SetActive(false);
                    }

                    var AudioTabPageContainer = settingsRootContainer.Find("Settings_Audio");
                    OriginalTabPageContainerRoot = AudioTabPageContainer;
                }
            }

            if (ModsTabPageContainer == null)
            {
                ModsTabPageContainer = ModsTabPageContainerRoot.transform.Find("Settings_Mod_Mask");
                if (ModsTabPageContainer == null)
                {
                    ModsTabPageContainer = ModsTabPageContainerRoot.transform.Find("Settings_General_Mask");
                    if (ModsTabPageContainer != null)
                    {
                        ModsTabPageContainer.name = "Settings_Mods_Mask";
                        // This object contains the actual components, we must modify these components to inject custom settings
                        ModsTabPageContainer = ModsTabPageContainer.GetChild(0);
                        ModsTabPageContainer.transform.name = "Settings_Mods_Layout";
                        ModConsole.Log("Successfully hooked Mods settings!", LogLevel.Verbose);
                    }
                }
            }
        }

        // Tries finding the tab items, and creates / finds them if null (and possible)
        private static void TryFetchModsTabs()
        {
            // GameMaster/_TSPUD_MENU(Clone)/Root/Settings/PageTitles (Toggles)

            if (settingsMenuTabContainer == null)
            {
                settingsMenuTabContainer = GameObject.Find(PageTitlesStr);
            }

            if (settingsMenuTabContainer != null)
            {
                // Try finding the mods menu tab
                ModsTabPageItem = GameObject.Find($"{PageTitlesStr}/{ModsButtonStr}");
                if (ModsTabPageItem == null)
                {
                    // Clone the Audio tab menu, and fork it into our own tab page
                    // We don't pick the General tab as otherwise the page will assume the active state
                    var audioTab = GameObject.Find($"{PageTitlesStr}/Audio (PageButton)");
                    if (audioTab == null)
                    {
                        ModConsole.Warn("Failed to find Audio tab! Was this a mistake?", LogLevel.Normal);
                        return;
                    }
                    ModsTabPageItem = GameObject.Instantiate(audioTab);

                    // Re-parent it, and re-name too
                    ModsTabPageItem.transform.parent = settingsMenuTabContainer.transform;
                    ModsTabPageItem.transform.SetSiblingIndex(settingsMenuTabContainer.transform.GetSiblingIndex() + 4); // 5th is mods
                    ModsTabPageItem.transform.name = ModsButtonStr;
                    ModsTabPageItem.transform.localScale = Vector3.one;
                    var textComponents = ModsTabPageItem.GetComponentsInChildren<TextMeshProUGUI>(true);
                    var localizers = ModsTabPageItem.GetComponentsInChildren<I2.Loc.Localize>(true);

                    // Disable localisation on custom tabs
                    for (int i = 0; i < localizers.Length; i++)
                    {
                        Component.Destroy(localizers[i]);
                    }

                    // Re-name it to "Mods"
                    for (int i = 0; i < textComponents.Length; i++)
                    {
                        var textComponent = textComponents[i];
                        textComponent.text = "Mods"; // TODO: i18n
                        // Manually copy font because it breaks for some reason
                        if (!textComponent.isActiveAndEnabled)
                        {
                            textComponent.font = textComponents[0].font;
                            textComponent.fontMaterial = textComponents[0].fontMaterial;
                        }
                    }
                }

                if (ModsTabPageItem != null)
                {
                    var toggleComponent = ModsTabPageItem.GetComponent<Toggle>();
                    
                    // Rebind the code to connect to our custom page
                    if (toggleComponent != null)
                    {
                        toggleComponent.onValueChanged.RemoveAllListeners();
                        toggleComponent.onValueChanged.AddListener(ActiveModPage);
                    }

                    // Disable audio page on all other buttons, this is due to us cloning from audio
                    var buttonToggles = ModsTabPageItem.transform.parent.GetComponentsInChildren<Toggle>();
                    for (int i = 0; i < buttonToggles.Length; i++)
                    {
                        // Ignore audio settings
                        if (buttonToggles[i] != null && buttonToggles[i].transform.name != "Audio (PageButton)")
                        {
                            buttonToggles[i].onValueChanged.AddListener(DisableOriginalPage);
                        }
                    }
                }
            }
        }

        // Injects a Mods tab into settings
        internal static void RegisterModsTab()
        {
            // Ensure the core refs (which we need to inject exist)
            TryFetchModsTabs();

            // Try force assigning the onClick Event
            if (ModsTabPageItem != null)
            {
                var toggleComponent = ModsTabPageItem.GetComponent<Toggle>();

                // Rebind the code to connect to our custom page
                if (toggleComponent != null)
                {
                    toggleComponent.onValueChanged.RemoveAllListeners();
                    toggleComponent.onValueChanged.AddListener(ActiveModPage);
                }

                // Disable audio page on all other buttons, this is due to us cloning from audio
                var buttonToggles = ModsTabPageItem.transform.parent.GetComponentsInChildren<Toggle>();
                for (int i = 0; i < buttonToggles.Length; i++)
                {
                    // Ignore audio settings
                    if (buttonToggles[i] != null && buttonToggles[i].transform.name != "Audio (PageButton)")
                    {
                        ModConsole.Log($"[{i}]:: { buttonToggles[i].transform.name }");
                        buttonToggles[i].onValueChanged.AddListener(DisableOriginalPage);
                    }
                }
            }
        }

        internal static void ActiveModPage(bool state)
        {
            ModConsole.Log("Activating mods page!");
            TryFetchModsPage();

            if (ModsTabPageContainerRoot != null)
            {
                ModsTabPageContainerRoot.gameObject.SetActive(state);
                if (OriginalTabPageContainerRoot != null)
                {
                    OriginalTabPageContainerRoot.gameObject.SetActive(!state);
                }
            }
        }

        // A hack workaround to fix the fact that not all pages disable the mods overlay
        internal static void DisableOriginalPage(bool state)
        {
            // ModConsole.Log("Disabling mods page!");
            TryFetchModsPage();

            if (ModsTabPageContainerRoot != null)
            {
                OriginalTabPageContainerRoot.gameObject.SetActive(!state);
            }
        }

        // TODO: Probably evolve into a struct or something
        public static void RegisterSettingsCategory(string category)
        {

        }

        // TODO: Shorthand methods for creating common controls


        public static void CreateHeader(string title)
        {

        }

        public static void CreateToggle(string title, ref bool toggleValue, Event callback)
        {

        }

        // TODO: Controls w/ rebinding
    }
}

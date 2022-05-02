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
        private static GameObject OriginalTabPageContainerRoot;
        private static GameObject ModsTabPageContainerRoot;
        private static GameObject ModsTabPageContainer;

        private static string PageTitlesStr = "PageTitles (Toggles)";
        private static string ModsButtonStr = "Mods (PageButton)";
        private static string ModsPageStr = "Settings_Mods";

        private static void TryFetchModsPage()
        {
            if (ModsTabPageContainerRoot == null)
            {
                // Try finding the mods page
                ModsTabPageContainerRoot = GameObject.Find($"Settings/{ModsPageStr}");
                if (ModsTabPageContainerRoot == null) // If it's null, try creating it
                {
                    var GeneralTabPageContainer = GameObject.Find("Settings/Settings_General");
                    OriginalTabPageContainerRoot = GeneralTabPageContainer;
                    if (GeneralTabPageContainer != null)
                    {
                        ModsTabPageContainerRoot = GameObject.Instantiate(GeneralTabPageContainer);

                        // Re-parent it, and re-name too
                        ModsTabPageContainerRoot.transform.parent = GeneralTabPageContainer.transform.parent;
                        ModsTabPageContainerRoot.transform.name = ModsPageStr;
                        ModsTabPageContainerRoot.transform.localScale = Vector3.one;
                        ModsTabPageContainerRoot.transform.position = GeneralTabPageContainer.transform.position;
                        ModsTabPageContainerRoot.SetActive(false);
                    }
                }
            }

            if (ModsTabPageContainer == null)
            {
                ModsTabPageContainer = GameObject.Find($"Settings/{ModsPageStr}/Settings_Mod_Mask");
                if (ModsTabPageContainer == null)
                {
                    ModsTabPageContainer = GameObject.Find($"Settings/{ModsPageStr}/Settings_General_Mask");
                    if (ModsTabPageContainer != null)
                    {
                        ModsTabPageContainer.transform.name = "Settings_Mods_Mask";
                        ModsTabPageContainer.transform.GetChild(0).transform.name = "Settings_Mods_Layout";
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

                    // Other tab menus dont hide our "forked" mods page, so let's fix it

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
            }
        }

        internal static void ActiveModPage(bool state)
        {
            TryFetchModsPage();

            if (ModsTabPageContainerRoot != null)
            {
                ModsTabPageContainerRoot.SetActive(state);
                if (OriginalTabPageContainerRoot != null)
                {
                    OriginalTabPageContainerRoot.SetActive(!state);
                }
            }
        }
        
        // TODO: Probably evolve into a struct or something
        public static void RegisterSettingsCategory(string category)
        {

        }

        // TODO: Shorthand methods for creating common controls

        // TODO: Controls w/ rebinding
    }
}

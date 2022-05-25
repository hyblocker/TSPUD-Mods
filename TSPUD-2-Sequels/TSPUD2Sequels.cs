using MelonLoader;
using ModThatLetsYouMod;
using TSPUD_2_Sequels;
using UnityEngine;

[assembly: MelonGame("Crows Crows Crows", "The Stanley Parable: Ultra Deluxe")]
[assembly: MelonInfo(typeof(TSPUD2Sequels), "TSP2 Sequals", "1.0.0", "Hyblocker", "https://github.com/hyblocker/TSPUD-Mods")]
[assembly: MelonAdditionalDependencies("ModThatLetsYouMod")] // This mod requires ModThatLetsYouMod to even function, since it uses it
namespace TSPUD_2_Sequels
{
    public class TSPUD2Sequels : MelonMod
    {

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            TSPUD2LogoPatch();
        }

        // Patches the debug settings tab
        private void TSPUD2LogoPatch()
        {
            var logoObj = GameObject.Find("GameMaster/_TSPUD_MENU(Clone)/Root/Main_Menu_TSP2/Opening Animator/Header_Text_Lower");
            var numberObj = GameObject.Find("GameMaster/_TSPUD_MENU(Clone)/Root/Main_Menu_TSP3/Main_Header/TSP_3_Header/Header_Text_Number");
            if (logoObj != null && numberObj != null)
            {
                var logoText = logoObj.GetComponent<TMPro.TextMeshProUGUI>();
                var numObjText = numberObj.GetComponent<TMPro.TextMeshProUGUI>();
                if (logoText != null)
                    logoText.text = $"THE STANLEY PARABLE <size=178><color=red>{numObjText.text}</color></size>";
            }
        }

    }
}
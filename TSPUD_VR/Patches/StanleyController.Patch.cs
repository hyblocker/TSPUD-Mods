#if false

using HarmonyLib;
using MelonLoader;
using ModThatLetsYouMod;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Valve.VR;

namespace TSPUD_VR.Patches
{
    public static class StanleyControllerPatch
    {
        /// <summary>
        /// Inverse of Stanley's scale relative to real life; i.e. transform real life coords to base game coords
        /// </summary>
        const float InvStanleyScaleRelativeToRealLife = 0.3511111111f;
        /// <summary>
        /// Stanley's scale relative to real life ; i.e. base game to real life
        /// </summary>
        const float StanleyScaleRelativeToRealLife = 1f / InvStanleyScaleRelativeToRealLife;

        public static void PatchController()
        {
            Hooking.Hook(typeof(StanleyController).GetMethod("Update", AccessTools.all), typeof(StanleyControllerPatch).GetMethod("StanleyInitPatch", AccessTools.all));
            Hooking.Hook(typeof(StanleyController).GetMethod("Movement", AccessTools.all), typeof(StanleyControllerPatch).GetMethod("MovementPatch", AccessTools.all));
            Hooking.Hook(typeof(StanleyController).GetMethod("Movement", AccessTools.all), typeof(StanleyControllerPatch).GetMethod("MovementPatchPre", AccessTools.all), isPrefix: true);

            Hooking.Hook(typeof(StanleyController).GetMethod("View", AccessTools.all), typeof(StanleyControllerPatch).GetMethod("VR_ViewPatch", AccessTools.all), isPrefix: true);
            // var viewPatcher = VRMod.Instance.HarmonyInstance.CreateReversePatcher(typeof(StanleyController).GetMethod("View", AccessTools.all), new HarmonyMethod(typeof(StanleyControllerPatch).GetMethod("VR_ViewPatch", AccessTools.all)));
            // MelonLogger.Msg(viewPatcher == null);
            // viewPatcher.Patch(HarmonyReversePatchType.Snapshot);


            // Fix FOV?
            // XRDevice.fovZoomFactor = 2f;

        }

        private static void StanleyInitPatch(StanleyController __instance)
        {
            // MelonLogger.Msg($"FOV: { __instance.cam.fieldOfView }");
        }

        private static void MovementPatch(StanleyController __instance)
        {
            // Set cam pos to floor ; HMD will set height instead because of VR!
            StanleyController.Instance.camParent.localPosition = new Vector3(0, -Player.CrouchedColliderHeight, 0);
            typeof(StanleyController).GetField("camParentOrigLocalPos", AccessTools.all).SetValue(StanleyController.Instance, new Vector3(0, -Player.CrouchedColliderHeight, 0));

            // In game, Stanley is 0.632 metres tall, while the average human is ~1.8m tall
            // Thus the world is about 2.8 times smaller in scale than real life
            // This becomes a problem in VR, where world scale is IMPORTANT
            // To work around this without breaking the core game + custom maps, let's try making stanley 2.8 times smaller
            __instance.transform.localScale = Vector3.one * InvStanleyScaleRelativeToRealLife;
        }
        private static void MovementPatchPre()
        {

            // Rework movement to use VR controller

            // Get the VR camera's yaw
            float VRYaw = StanleyController.Instance.camTransform.rotation.eulerAngles.y;

            // Use it for the move direction

            // MelonLogger.Msg($"Yaw: {VRYaw}");

            var movementInput = (Vector3)typeof(StanleyController).GetField("movementInput", AccessTools.all).GetValue(StanleyController.Instance);

            // Vector2 leftThumbstickState = Vector2.zero;
            // bool valid = VRInputHelper.LeftHand.isValid // the device is still valid
            //             && VRInputHelper.LeftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftThumbstickState);
            // movementInput.x = leftThumbstickState.x;
            // movementInput.y = leftThumbstickState.y;

            movementInput.x += SteamVR_Actions.default_MoveLeft.active ? -1f : 0f;
            movementInput.x += SteamVR_Actions.default_MoveRight.active ? 1f : 0f;
            movementInput.y += SteamVR_Actions.default_MoveForward.active ? 1f : 0f;
            movementInput.y += SteamVR_Actions.default_MoveBackwards.active ? -1f : 0f;
            movementInput.Normalize();
            MelonLogger.Msg($"Mov: X : { movementInput.x }\tY : {movementInput.y}");
        }

        private static bool VR_ViewPatch(StanleyController __instance)
        {
            // Don't run the original view code
            return false;
        }
    }

    [HarmonyPatch(typeof(StanleyController))]
    [HarmonyPatch("Movement")]
    class StanleyControllerReversePatches
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            int startIndex = -1, endIndex = -1;

            var codes = new List<CodeInstruction>(instructions);
            for (int i = 0; i < codes.Count; i++)
            {
                // +4 => offset to movementZ ptr
                if (codes[i].opcode == OpCodes.Ldflda && codes[i + 2 + 4].opcode == OpCodes.Stfld)
                {
                    startIndex = i - 1; // instruction before ldflda
                    endIndex = i + 2 + 4;   // stfld
                    break;
                }
            }

            if (startIndex > -1 && endIndex > -1)
            {
                for (int i = startIndex; i < endIndex + 1; i++)
                {
                    codes[i].opcode = OpCodes.Nop;
                }
            }

            return codes.AsEnumerable();
        }
    }
}

#endif
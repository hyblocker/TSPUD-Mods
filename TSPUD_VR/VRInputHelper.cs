using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR;
using Valve.VR;

namespace TSPUD_VR
{
    public static class VRInputHelper
    {
        public static InputDevice LeftHand;
        public static InputDevice RightHand;

        public static SteamVR_ActionSet actionSet;

        internal static void Init()
        {

        }

        internal static void Update()
        {
            var leftHandDevices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);

            var rightHandDevices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);
        }
    }
}

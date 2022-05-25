//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Action_Boolean p_default_InteractUI;
        
        private static SteamVR_Action_Single p_default_InteractWorld;
        
        private static SteamVR_Action_Boolean p_default_HeadsetOnHead;
        
        private static SteamVR_Action_Boolean p_default_Jump;
        
        private static SteamVR_Action_Boolean p_default_MoveForward;
        
        private static SteamVR_Action_Boolean p_default_MoveLeft;
        
        private static SteamVR_Action_Boolean p_default_MoveRight;
        
        private static SteamVR_Action_Boolean p_default_MoveBackwards;
        
        private static SteamVR_Action_Pose p_default_LeftHandPose;
        
        private static SteamVR_Action_Pose p_default_RightHandPose;
        
        private static SteamVR_Action_Vibration p_default_Haptic;
        
        public static SteamVR_Action_Boolean default_InteractUI
        {
            get
            {
                return SteamVR_Actions.p_default_InteractUI.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Single default_InteractWorld
        {
            get
            {
                return SteamVR_Actions.p_default_InteractWorld.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Boolean default_HeadsetOnHead
        {
            get
            {
                return SteamVR_Actions.p_default_HeadsetOnHead.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_Jump
        {
            get
            {
                return SteamVR_Actions.p_default_Jump.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_MoveForward
        {
            get
            {
                return SteamVR_Actions.p_default_MoveForward.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_MoveLeft
        {
            get
            {
                return SteamVR_Actions.p_default_MoveLeft.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_MoveRight
        {
            get
            {
                return SteamVR_Actions.p_default_MoveRight.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_MoveBackwards
        {
            get
            {
                return SteamVR_Actions.p_default_MoveBackwards.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Pose default_LeftHandPose
        {
            get
            {
                return SteamVR_Actions.p_default_LeftHandPose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Pose default_RightHandPose
        {
            get
            {
                return SteamVR_Actions.p_default_RightHandPose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Vibration default_Haptic
        {
            get
            {
                return SteamVR_Actions.p_default_Haptic.GetCopy<SteamVR_Action_Vibration>();
            }
        }
        
        private static void InitializeActionArrays()
        {
            Valve.VR.SteamVR_Input.actions = new Valve.VR.SteamVR_Action[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_InteractWorld,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_Jump,
                    SteamVR_Actions.default_MoveForward,
                    SteamVR_Actions.default_MoveLeft,
                    SteamVR_Actions.default_MoveRight,
                    SteamVR_Actions.default_MoveBackwards,
                    SteamVR_Actions.default_LeftHandPose,
                    SteamVR_Actions.default_RightHandPose,
                    SteamVR_Actions.default_Haptic};
            Valve.VR.SteamVR_Input.actionsIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_InteractWorld,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_Jump,
                    SteamVR_Actions.default_MoveForward,
                    SteamVR_Actions.default_MoveLeft,
                    SteamVR_Actions.default_MoveRight,
                    SteamVR_Actions.default_MoveBackwards,
                    SteamVR_Actions.default_LeftHandPose,
                    SteamVR_Actions.default_RightHandPose};
            Valve.VR.SteamVR_Input.actionsOut = new Valve.VR.ISteamVR_Action_Out[] {
                    SteamVR_Actions.default_Haptic};
            Valve.VR.SteamVR_Input.actionsVibration = new Valve.VR.SteamVR_Action_Vibration[] {
                    SteamVR_Actions.default_Haptic};
            Valve.VR.SteamVR_Input.actionsPose = new Valve.VR.SteamVR_Action_Pose[] {
                    SteamVR_Actions.default_LeftHandPose,
                    SteamVR_Actions.default_RightHandPose};
            Valve.VR.SteamVR_Input.actionsBoolean = new Valve.VR.SteamVR_Action_Boolean[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_Jump,
                    SteamVR_Actions.default_MoveForward,
                    SteamVR_Actions.default_MoveLeft,
                    SteamVR_Actions.default_MoveRight,
                    SteamVR_Actions.default_MoveBackwards};
            Valve.VR.SteamVR_Input.actionsSingle = new Valve.VR.SteamVR_Action_Single[] {
                    SteamVR_Actions.default_InteractWorld};
            Valve.VR.SteamVR_Input.actionsVector2 = new Valve.VR.SteamVR_Action_Vector2[0];
            Valve.VR.SteamVR_Input.actionsVector3 = new Valve.VR.SteamVR_Action_Vector3[0];
            Valve.VR.SteamVR_Input.actionsSkeleton = new Valve.VR.SteamVR_Action_Skeleton[0];
            Valve.VR.SteamVR_Input.actionsNonPoseNonSkeletonIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_InteractWorld,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_Jump,
                    SteamVR_Actions.default_MoveForward,
                    SteamVR_Actions.default_MoveLeft,
                    SteamVR_Actions.default_MoveRight,
                    SteamVR_Actions.default_MoveBackwards};
        }
        
        private static void PreInitActions()
        {
            SteamVR_Actions.p_default_InteractUI = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/InteractUI")));
            SteamVR_Actions.p_default_InteractWorld = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/default/in/InteractWorld")));
            SteamVR_Actions.p_default_HeadsetOnHead = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/HeadsetOnHead")));
            SteamVR_Actions.p_default_Jump = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Jump")));
            SteamVR_Actions.p_default_MoveForward = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/MoveForward")));
            SteamVR_Actions.p_default_MoveLeft = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/MoveLeft")));
            SteamVR_Actions.p_default_MoveRight = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/MoveRight")));
            SteamVR_Actions.p_default_MoveBackwards = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/MoveBackwards")));
            SteamVR_Actions.p_default_LeftHandPose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/default/in/LeftHandPose")));
            SteamVR_Actions.p_default_RightHandPose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/default/in/RightHandPose")));
            SteamVR_Actions.p_default_Haptic = ((SteamVR_Action_Vibration)(SteamVR_Action.Create<SteamVR_Action_Vibration>("/actions/default/out/Haptic")));
        }
    }
}
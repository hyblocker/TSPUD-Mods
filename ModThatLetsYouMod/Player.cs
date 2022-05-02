using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModThatLetsYouMod
{
    /// <summary>
    /// A small helper class to help you interface with <see cref="StanleyController"/> more easily
    /// </summary>
    public static class Player
    {
        /// <summary>
        /// The height of Stanley's collider when standing
        /// </summary>
        public static float UncrouchedColliderHeight
        {
            get
            {
                return (float)typeof(StanleyController).GetField("uncrouchedColliderHeight", AccessTools.all).GetValue(StanleyController.Instance);
            }
        }

        /// <summary>
        /// The height of Stanley's collider when crouching
        /// </summary>
        public static float CrouchedColliderHeight
        {
            get
            {
                return (float)typeof(StanleyController).GetField("crouchedColliderHeight", AccessTools.all).GetValue(StanleyController.Instance);
            }
        }

        /// <summary>
        /// Stanley's PlayerController
        /// </summary>
        public static CharacterController Character
        {
            get
            {
                return (CharacterController)typeof(StanleyController).GetField("character", AccessTools.all).GetValue(StanleyController.Instance);
            }
        }
    }
}

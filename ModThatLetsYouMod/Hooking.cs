using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModThatLetsYouMod
{
    public class Hooking
    {
        // Internal framework Harmony instance
        private static HarmonyLib.Harmony baseHarmony;
        internal static void SetHarmony(HarmonyLib.Harmony instance) => baseHarmony = instance;

        /// <summary>
        /// Convenient method to hook into a method using Harmony
        /// </summary>
        /// <param name="original">The original method to inject into</param>
        /// <param name="patch">The new code to patch into the original method</param>
        /// <param name="isPrefix">Whether to inject the patch before (prefix) or after (postfix) the original method</param>
        public static void Hook(MethodInfo original, MethodInfo patch, bool isPrefix = false)
        {
            if (baseHarmony == null)
            {
                // We should never reach here, but in such a scenario, delay hooking events
                // TODO: Revisit once deferred hooks have been implemented
                throw new NotImplementedException("Deferred Hooking isn't yet implemented. Also how did you even get here?");
            }

            // Try fetching the calling mod's Harmony instance
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            MelonMod callingMod = MelonHandler.Mods.FirstOrDefault(x => x.Assembly.FullName == callingAssembly.FullName);
            HarmonyLib.Harmony harmony = callingMod != null ? callingMod.HarmonyInstance : baseHarmony;

            HarmonyMethod prefix = isPrefix ? new HarmonyMethod(patch) : null;
            HarmonyMethod postfix = isPrefix ? null : new HarmonyMethod(patch);
            harmony.Patch(original, prefix, postfix);

            ModConsole.Log($"New patch {(isPrefix ? "PREFIX" : "POSTFIX")} on {original.DeclaringType.Name}.{original.Name} to {patch.DeclaringType.Name}.{patch.Name}", LogLevel.Debug);
        }
    }
}

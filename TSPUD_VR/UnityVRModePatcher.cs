using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TSPUD_VR
{
    internal class UnityVRModePatcher
    {
        /// <summary>
        /// Pathces the globalgamemanagers file to force VR mode
        /// </summary>
        public static void PatchGlobalGameManagers()
        {
            // MelonLoader.MelonUtils.UserDataDirectory
            var gameDataDir = Path.Combine(MelonLoader.MelonUtils.GameDirectory, $"{Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().ProcessName)}_Data");
            var am = new AssetsManager();
            var inst = am.LoadAssetsFile(Path.Combine(gameDataDir, "globalgamemanagers"), true);

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TSPUD_VR.Resources.classdata.tpk");

            // am.LoadClassPackage("classdata.tpk");
            am.LoadClassPackage(stream);
            am.LoadClassDatabaseFromPackage(inst.file.typeTree.unityVersion);

            foreach (var inf in inst.table.GetAssetsOfType((int)AssetClassID.GameObject))
            {
                var baseField = am.GetTypeInstance(inst, inf).GetBaseField();

                var name = baseField.Get("m_Name").GetValue().AsString();
                Console.WriteLine(name);
            }

            am.UnloadAllAssetsFiles();
        }
    }
}

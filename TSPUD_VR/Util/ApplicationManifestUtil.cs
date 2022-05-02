using ModThatLetsYouMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Valve.Newtonsoft.Json;
using Valve.VR;

namespace TSPUD_VR.Util
{
    public static class ApplicationManifestUtil
    {
        /// <summary>
        /// Registers the app with SteamVR
        /// </summary>
        public static void UpdateManifest(string manifestPath, string appKey, string imagePath, string name, string description, int steamAppId = 0, bool steamBuild = false)
        {
            try
            {
                var launchType = steamBuild ? GetSteamLaunchString(steamAppId) : GetBinaryLaunchString();
                var appManifestContent = $@"{{
                                            ""source"": ""builtin"",
                                            ""applications"": [{{
                                                ""app_key"": {JsonConvert.ToString(appKey)},
                                                ""image_path"": {JsonConvert.ToString(imagePath)},
                                                {launchType}
                                                ""last_played_time"":""{DateUtils.CurrentUnixTimestamp()}"",
                                                ""strings"": {{
                                                    ""en_us"": {{
                                                        ""name"": {JsonConvert.ToString(name)}
                                                    }}
                                                }}
                                            }}]
                                        }}";

                File.WriteAllText(manifestPath, appManifestContent);

                var error = OpenVR.Applications.AddApplicationManifest(manifestPath, false);

                var processId = System.Diagnostics.Process.GetCurrentProcess().Id;
                var applicationIdentifyErr = OpenVR.Applications.IdentifyApplication((uint)processId, appKey);
            }
            catch (Exception)
            { }
        }
        private static string GetSteamLaunchString(int steamAppId)
        {
            return $@"""launch_type"": ""url"",
                      ""url"": ""steam://launch/{steamAppId}/VR"",";
        }

        private static string GetBinaryLaunchString()
        {
            var workingDir = Directory.GetCurrentDirectory();
            var executablePath = Assembly.GetExecutingAssembly().Location;
            return $@"""launch_type"": ""binary"",
                      ""binary_path_windows"": {JsonConvert.ToString(executablePath)},
                      ""working_directory"": {JsonConvert.ToString(workingDir)},";
        }
    }
}

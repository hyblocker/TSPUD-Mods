using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModThatLetsYouMod
{
    /// <summary>
    /// A class designed to make handling settings easier
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Initializes mod settings into the specified object.
        /// </summary>
        /// <param name="modSettings">A user defined settings object to be populated</param>
        /// <param name="suffix">Suffix for settings file's name</param>
        public static void Initialize<T>(out T modSettings, string suffix = "_preferences") where T : new()
        {
            // Init the object
            modSettings = new T();

            string modName = Assembly.GetCallingAssembly().GetName().Name;
            string jsonPathRoot = Path.Combine(MelonLoader.MelonUtils.UserDataDirectory, modName);
            string jsonPath = Path.Combine(jsonPathRoot, $"{modName}{suffix}.json");
            if (File.Exists(jsonPath))
            {
                try
                {
                    string jsonRaw = File.ReadAllText(jsonPath);
                    modSettings = JsonUtility.FromJson<T>(jsonRaw);
                }
                catch (Exception ex)
                {
                    ModConsole.Error($"Failed to read settings for {modName}!\n{ex.Message}\n{ex.StackTrace}", LogLevel.Normal);
                }
            }
            else
            {
                ModConsole.Log($"Settings for {modName} couldn't be found! Writing default settings...", LogLevel.Verbose);
                try
                {
                    // Ensure the directories exist
                    Directory.CreateDirectory(jsonPathRoot);
                    // Save default settings
                    string defaultJson = JsonUtility.ToJson(modSettings, true);
                    File.WriteAllText(jsonPath, defaultJson);
                }
                catch (Exception ex)
                {
                    ModConsole.Error($"Failed to write default settings for {modName}!\n{ex.Message}\n{ex.StackTrace}", LogLevel.Normal);
                }
            }
        }

        /// <summary>
        /// Saves the settings object
        /// </summary>
        /// <param name="modSettings">A user defined settings object to be saved</param>
        /// <param name="suffix">Suffix for settings file's name</param>
        public static void Save<T>(T modSettings, string suffix = "_preferences")
        {
            string modName = Assembly.GetCallingAssembly().GetName().Name;
            string jsonPathRoot = Path.Combine(MelonLoader.MelonUtils.UserDataDirectory, modName);
            string jsonPath = Path.Combine(jsonPathRoot, $"{modName}{suffix}.json");
            try
            {
                Directory.CreateDirectory(jsonPathRoot);
                string defaultJson = JsonUtility.ToJson(modSettings, true);
                File.WriteAllText(jsonPath, defaultJson);
                ModConsole.Log($"Saved settings for {modName}!", LogLevel.Verbose);
            }
            catch (Exception ex)
            {
                ModConsole.Error($"Failed to write default settings for {modName}!\n{ex.Message}\n{ex.StackTrace}", LogLevel.Normal);
            }
        }
    }
}

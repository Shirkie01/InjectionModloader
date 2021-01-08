using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace InjectionModLoaderFW
{
    /// <summary>
    /// InjectionModLoader.dll Must be placed in the [Game]_Data/Managed/ directory, and an IL editor used to inject a call to ModLoader.LoadMods().
    /// </summary>
    public class ModLoader
    {
        private static readonly string format = $"[{nameof(ModLoader)}]" + " - {0}";

        /// <summary>
        /// Loads mods in the Mods directory, or creates it if it doesn't exist.
        /// </summary>
        public static void LoadMods()
        {
            try
            {
                var modDirectory = new DirectoryInfo("Mods");
                
                // If the mod directory doesn't exist, create it and bail.
                if(!modDirectory.Exists)
                {
                    modDirectory.Create();
                    return;
                }                
                
                var files = modDirectory.GetFiles("*.dll", SearchOption.AllDirectories);

                Debug.LogFormat(format, $"Loading {files.Length} mods.");

                // Load the mods by creating a separate GameObject for each one.
                foreach (var file in files)
                {
                    var assem = Assembly.LoadFrom(file.FullName);
                    var types = assem.GetTypes();
                    foreach (var type in types)
                    {
                        try
                        {
                            GameObject go = new GameObject(file.FullName);
                            var component = go.AddComponent(type);                            
                        }
                        catch(Exception e)
                        {
                            // If a mod has an exception when loading, catch it so we don't break the loader.
                            Debug.LogErrorFormat(format, $"Failed to load {file.FullName}: " + Environment.NewLine + e);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                // Catch all exceptions so we don't break the game.
                Debug.LogErrorFormat(format, "Failed to load mods." + Environment.NewLine + e);
            }
        }              
        
    }
}
// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace MyHalp.Editor.MyCooker
{
    /// <summary>
    /// The MyCooker manager class.
    /// </summary>
    public class MyCookerManager
    {
        private struct SaveObject
        {
            public List<MyCookerPreset> Presets { get; set; }
            public int SelectedPreset { get; set; }
        }

        /// <summary>
        /// All created presets.
        /// </summary>
        public List<MyCookerPreset> Presets = new List<MyCookerPreset>();

        /// <summary>
        /// The current selected preset.
        /// </summary>
        public MyCookerPreset SelectedPreset;

        /// <summary>
        /// The current selected target.
        /// </summary>
        public MyCookerPreset.Target SelectedTarget;

        /// <summary>
        /// Saves all configuration changes.
        /// </summary>
        public void Save()
        {
            // build save object
            var obj = new SaveObject
            {
                Presets = Presets
            };
            obj.SelectedPreset = obj.Presets.IndexOf(SelectedPreset);

            var data = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText("mycooker.json", data); // TODO: check if this wokrks on mac/linux
        }

        /// <summary>
        /// Loads previous configuration.
        /// </summary>
        public void Load()
        {
            if (File.Exists("mycooker.json"))
            {
                var data = File.ReadAllText("mycooker.json");
                try
                {
                    var obj = JsonConvert.DeserializeObject<SaveObject>(data);

                    Presets = obj.Presets;

                    if(Presets.Count > 0 && obj.SelectedPreset >= 0)
                        SelectedPreset = obj.Presets[obj.SelectedPreset];
                }
                catch
                {
                    Debug.LogWarning("Failed to load MyCooker configuration");
                }
            }
            else
            {
                Debug.LogWarning("MyCooker configuration not found, created new one");
                File.WriteAllText("mycooker.json", "{}");
                Presets = new List<MyCookerPreset>();
            }
        }

        /// <summary>
        /// Builds the selected preset.
        /// </summary>
        public void Build()
        {
            Build(false);
        }

        /// <summary>
        /// Builds scripts of the selected preset.
        /// </summary>
        public void BuildScripts()
        {
            Build(true);
        }

        /// <summary>
        /// Resets defines.
        /// </summary>
        public void ResetDefines()
        {
            BuildPipelineHelper.SetDefines(Presets.ToArray());
        }

        // private
        private void Build(bool scriptsOnly)
        {
            // BUG: this may be invalid for some platforms
            var lastDirectives = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);

            // TODO: auto start option(warning: use proper working dir!)

            foreach (var target in SelectedPreset.Targets)
            {
                try
                {
                    if (scriptsOnly)
                    {
                        // TODO: monodevelop support
                        // TODO: xcode support

                        MicrosoftBuild.CompileSolution(target.DefineSymbols);
                    }
                    else
                    {
                        BuildPipelineHelper.Build(target);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to build target: " + target.Name + " error: " + ex);
                }
            }

            if (!scriptsOnly)
            {
                // reset the directives
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, lastDirectives);
            }
            else
            {
                try
                {
                    Debug.Log("Building scripts only...");
                    //BuildPipelineHelper.BuildScripts();
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to script only build error: " + ex);
                }
            }

            // increase build number TODO: build counting
        }
    }
}

// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // private
        private void Build(bool scriptsOnly)
        {
            var lastDirectives = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone); // TODO: this may be invalid for some platforms
            var scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
            
            foreach (var target in SelectedPreset.Targets)
            {
                try
                {
                    var defines = string.Join(";", DefineSymbolsForTarget(target));
                    var options = OptionsForTarget(target);

                    // use has some hacks, so we can use .exe for all platforms
                    // it will be changed by the build pipeline
                    var outputPath = Application.dataPath.Replace("Assets", "build/" + target.OutputName);
                    var outputPathName = outputPath + "/" + target.ExecutableName + ".exe";

                    if (scriptsOnly)
                    {
                        options |= BuildOptions.BuildScriptsOnly;

                        if (!File.Exists(outputPathName))
                        {
                            Debug.Log("Failed to build target: " + target.Name + " error: there is no prebuilt application.");
                            continue;
                        }
                    }
                    else
                    {
                        // delete the output directory if exists
                        if (File.Exists(outputPathName))
                        {
                            // delete the directory
                            Directory.Delete(outputPath, true);
                        }
                    }

                    // increase build number TODO: build counting
                    //target.BuildNumber++;
                    //Save();

                    // TODO: use separate CLI nogfx/headless unity through console/terminal with parameters 
                    // to build the game(do not block the current unity instance)

                    // set defines
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);

                    // build!
                    BuildPipeline.BuildPlayer(scenes.ToArray(), outputPathName, target.BuildTarget, options);
                }
                catch (Exception ex)
                {
                    Debug.Log("Failed to build target: " + target.Name + " error: " + ex);
                }
            }

            // reset the directives
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, lastDirectives);

            // TODO: auto start option(warning: use proper working dir!)
        }

        // private
        private static string[] DefineSymbolsForTarget(MyCookerPreset.Target target)
        {
            var defines = new List<string>
            {
                "UNITY_3D", "CATCH_EXCEPTIONS"
            };
            
            switch (target.Type)
            {
                case MyCookerPreset.Target.BuildType.Debug:
                    defines.Add("DEBUG");
                    break;
                case MyCookerPreset.Target.BuildType.Release:
                    defines.Add("RELEASE");
                    break;
                case MyCookerPreset.Target.BuildType.Shipping:
                    defines.Add("SHIPPING");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (target.BuildTarget)
            {
                case BuildTarget.StandaloneOSXIntel:
                    defines.Add("OSX");
                    break;
                case BuildTarget.StandaloneOSXUniversal:
                case BuildTarget.StandaloneOSXIntel64:
                    defines.Add("OSX");
                    defines.Add("OSX64");
                    break;
                case BuildTarget.StandaloneWindows:
                    defines.Add("WINDOWS");
                    break;
                case BuildTarget.StandaloneWindows64:
                    defines.Add("WINDOWS");
                    defines.Add("WINDOWS64");
                    break;
                case BuildTarget.StandaloneLinux:
                    defines.Add("LINUX");
                    break;
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.StandaloneLinuxUniversal:
                    defines.Add("LINUX");
                    defines.Add("LINUX64");
                    break;
                case BuildTarget.iOS:
                    defines.Add("IOS");
                    break;
                case BuildTarget.Android:
                    defines.Add("ANDROID");
                    break;
                case BuildTarget.WebGL:
                    defines.Add("WEBGL");
                    break;
                    // TODO: we need more supported platforms
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return defines.ToArray();
        }

        // private
        private static BuildOptions OptionsForTarget(MyCookerPreset.Target target)
        {
            var options = BuildOptions.None;

            if (target.Headless)
                options |= BuildOptions.EnableHeadlessMode;

            switch (target.Type)
            {
                case MyCookerPreset.Target.BuildType.Debug:
                    options |= BuildOptions.Development;
                    options |= BuildOptions.AllowDebugging;
                    break;
                case MyCookerPreset.Target.BuildType.Release:
                    break;
                case MyCookerPreset.Target.BuildType.Shipping:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return options;
        }
    }
}


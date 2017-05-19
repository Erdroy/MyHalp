// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace MyHalp.Editor.MyCooker
{
    /// <summary>
    /// Unity build pipeline helper class
    /// </summary>
    internal static class BuildPipelineHelper
    {
        /// <summary>
        /// Queues target to scripts only build.
        /// </summary>
        /// <param name="target">The target.</param>
        public static void QueueBuildScripts(MyCookerPreset.Target target)
        {
            // TODO: if first, clear Temp/Cooker directory, this will fix some issues that would exist

            // check support
            if (target.BuildTarget != BuildTarget.StandaloneWindows
                && target.BuildTarget != BuildTarget.StandaloneWindows64
                && target.BuildTarget != BuildTarget.StandaloneLinux
                && target.BuildTarget != BuildTarget.StandaloneLinux64
                && target.BuildTarget != BuildTarget.StandaloneLinuxUniversal)
            {
                Debug.LogError("Failed to queue target: " + target.Name + " error: scripts only build is supported only for windows and linux.");
                return;
            }

            // TODO: create directory in Temp/Cooker/QueuedTargets(if doesn't exist)
            // TODO: add json file with the contents of `target`

            //ToBuild.Add(target);
        }

        /// <summary>
        /// Builds scripts only queued targets.
        /// </summary>
        public static void BuildScripts()
        {
            EditorPrefs.SetBool("ScriptsOnlyBuild", true);

            // TODO: load and delete first file in Temp/Cooker/QueuedTargets

            //StartBuildTarget(ToBuild[0]);
        }

        private static void StartBuildTarget(MyCookerPreset.Target target)
        {
            var defines = string.Join(";", DefineSymbolsForTarget(target));

            // use has some hacks, so we can use .exe for all platforms
            // it will be changed by the build pipeline
            var outputPath = Application.dataPath.Replace("Assets", "build/" + target.OutputName);
            var outputPathName = outputPath + "/" + target.ExecutableName + ".exe";

            if (!File.Exists(outputPathName))
            {
                Debug.LogError("Failed to build target: " + target.Name + " error: no prebuilt application found.");
                return;
            }

            // set define symbols and refresh
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
            AssetDatabase.Refresh();

            // force recompile
            ScriptsReloader.ForceReload();
        }

        [DidReloadScripts]
        private static void OnScriptsReloaded()
        {
           /* if (EditorPrefs.GetBool("ScriptsOnlyBuild"))
            {
                try
                {
                    var target = ToBuild[0];
                    Debug.Log("Recompiled " + target.Name);

                    // TODO: copy assembiles
                    // TODO: delete the current file
                    // TODO: load and run first file in Temp/Cooker/QueuedTargets

                    ToBuild.RemoveAt(0);
                    if (ToBuild.Count > 0)
                    {
                        StartBuildTarget(ToBuild[0]);
                    }
                    else
                    {
                        Debug.Log("Scripts only build done. All targets has been built.");

                        // finished building
                        EditorPrefs.SetBool("ScriptsOnlyBuild", false);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log("Failed to build: " + ex);
                    EditorPrefs.SetBool("ScriptsOnlyBuild", false);
                }
            }*/
        }

        /// <summary>
        /// Builds target.
        /// </summary>
        /// <param name="target">The target.</param>
        public static void Build(MyCookerPreset.Target target)
        {
            var scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
            var defines = string.Join(";", DefineSymbolsForTarget(target));
            var options = OptionsForTarget(target);

            // use has some hacks, so we can use .exe for all platforms
            // it will be changed by the build pipeline
            var outputPath = Application.dataPath.Replace("Assets", "build/" + target.OutputName);
            var outputPathName = outputPath + "/" + target.ExecutableName + ".exe";

            // delete the output directory if exists
            if (File.Exists(outputPathName))
            {
                // delete the directory
                Directory.Delete(outputPath, true);
            }

            // increase build number TODO: build counting
            //target.BuildNumber++;
            //Save();

            // TODO: use separate CLI nogfx/headless unity through console/terminal with parameters 
            // to build the game(do not block the current unity instance)

            // set define symbols and refresh
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
            AssetDatabase.Refresh();

            // build!
            BuildPipeline.BuildPlayer(scenes.ToArray(), outputPathName, target.BuildTarget, options);
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

            defines.AddRange(target.DefineSymbols);

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

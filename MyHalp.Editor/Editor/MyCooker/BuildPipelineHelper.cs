// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MyHalp.Editor.MyCooker
{
    /// <summary>
    /// Unity build pipeline helper class
    /// </summary>
    internal static class BuildPipelineHelper
    {
        // private
        private static string BuildPath => Application.dataPath.Replace("Assets", "build/");

        /// <summary>
        /// Builds target.
        /// </summary>
        /// <param name="target">The target.</param>
        public static void Build(MyCookerPreset.Target target)
        {
            if (!string.IsNullOrEmpty(target.PreBuildAction))
                CallBuildEvent(target.PreBuildAction);

            var scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
            var defines = string.Join(";", DefineSymbolsForTarget(target));
            var options = OptionsForTarget(target);

            // use has some hacks, so we can use .exe for all platforms
            // it will be changed by the build pipeline
            var outputPath = BuildPath + target.OutputName;
            var outputPathName = outputPath + "/" + target.ExecutableName + ".exe";

            // delete the output directory if exists
            if (File.Exists(outputPathName))
            {
                // delete the directory
                Directory.Delete(outputPath, true);
            }

            // TODO: use separate CLI nogfx/headless unity through console/terminal with parameters 
            // to build the game(do not block the current unity instance)

            // set define symbols and refresh
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
            AssetDatabase.Refresh();

            // build!
            BuildPipeline.BuildPlayer(scenes.ToArray(), outputPathName, target.BuildTarget, options);
            
            if (target.BuildContent)
            {
                var assetBundleOutput = outputPath + "/" + target.ContentDirectoryName;

                // build clean content directory if needed
                if (Directory.Exists(assetBundleOutput))
                    Directory.Delete(assetBundleOutput, true);

                Directory.CreateDirectory(assetBundleOutput);

                // build content
                BuildPipeline.BuildAssetBundles(assetBundleOutput, target.ContentBuildOptions, target.BuildTarget);
            }

            if (!string.IsNullOrEmpty(target.PostBuildAction))
                CallBuildEvent(target.PostBuildAction);
        }

        /// <summary>
        /// Sets defines for all presets.
        /// </summary>
        public static void SetDefines(MyCookerPreset[] presets)
        {
            var defines = new List<string>();

            foreach (var preset in presets)
            {
                foreach (var target in preset.Targets)
                {
                    var defs = DefineSymbolsForTarget(target);

                    foreach (var define in defs)
                    {
                        if (!defines.Contains(define))
                            defines.Add(define);
                    }
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, string.Join(";", defines.ToArray()));
        }

        /// <summary>
        /// Uses defines from given target.
        /// </summary>
        public static void SetDefines(MyCookerPreset.Target target)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, string.Join(";", DefineSymbolsForTarget(target)));
        }

        // private
        private static void CallBuildEvent(string commands)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C \" " + commands + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            process.StartInfo = startInfo;
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
                Debug.Log(output);
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


// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
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
        // private
        private static string CookerTemp => Application.dataPath.Replace("Assets", "Temp/MyCooker/");

        // private
        private static string BuildPath => Application.dataPath.Replace("Assets", "build/");

        // private
        private static string AssembliesPath => Application.dataPath.Replace("Assets", "Library/ScriptAssemblies/");

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
        }

        /// <summary>
        /// Builds scripts only queued targets.
        /// </summary>
        public static void BuildScripts()
        {
            StartScriptOnlyBuild(GetCurrentTarget());
        }

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

            if (!Directory.Exists(CookerTemp))
                Directory.CreateDirectory(CookerTemp);

            var targetCount = GetTargetCount();

            // add json file with the contents of `target`
            var json = JsonConvert.SerializeObject(target);
            File.WriteAllText(CookerTemp + "target_" + targetCount + ".json", json);
        }

        // private
        private static void StartScriptOnlyBuild(MyCookerPreset.Target target)
        {
            var defines = string.Join(";", DefineSymbolsForTarget(target));

            // use has some hacks, so we can use .exe for all platforms
            // it will be changed by the build pipeline
            var outputPathName = BuildPath + target.OutputName + "/" + target.ExecutableName + ".exe"; // TODO: proper file check (extension is invalid for non-windows targets)

            if (!File.Exists(outputPathName))
            {
                EditorPrefs.SetBool("ScriptsOnlyBuild", false);
                Debug.LogError("Failed to build target: " + target.Name + " error: no prebuilt application found.");
                return;
            }

            // say that we are building the script only way
            EditorPrefs.SetBool("ScriptsOnlyBuild", true);

            // set define symbols and refresh
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
            AssetDatabase.Refresh();

            // force recompile
            ScriptsReloader.ForceReload();
        }

        // private
        [DidReloadScripts, UsedImplicitly]
        private static void OnScriptsReloaded()
        {
            if (EditorPrefs.GetBool("ScriptsOnlyBuild"))
            {
                EditorPrefs.SetBool("ScriptsOnlyBuild", false);
                
                try
                {
                    if (GetTargetCount() == 0)
                        return;

                    // TODO: fix issues when editor is closed in unexpected way or any build fails. Do:
                    // - check if this is expected build
                    // - check if there is no any errors (check last and current assembly version?)
                    // then everything should be good

                    // read first target file
                    var target = GetCurrentTarget();
                    
                    // say that we have done building
                    Debug.Log("Recompiled " + target.Name);

                    // copy assembiles
                    CopyAssemblies();

                    // delete the current file
                    DeleteCurrentTarget();

                    // check if there is any other target left for building
                    if (GetTargetCount() > 0)
                    {
                        // load and run first file in cooker temp dir
                        StartScriptOnlyBuild(GetCurrentTarget());
                    }
                    else
                    {
                        // finished building
                        Debug.Log("Scripts only build done. All targets has been built.");

                        // clean temp dir
                        CleanTempDir();
                    }
                }
                catch (Exception ex)
                {
                    EditorPrefs.SetBool("ScriptsOnlyBuild", false);
                    Debug.Log("Failed to build: " + ex);
                }
            }
        }

        // private
        private static MyCookerPreset.Target GetCurrentTarget()
        {
            var files = Directory.GetFiles(CookerTemp).ToList();

            files.Sort();
            files.Reverse();

            foreach (var file in files)
            {
                if (file.Contains("target_"))
                {
                    return JsonConvert.DeserializeObject<MyCookerPreset.Target>(File.ReadAllText(file));
                }
            }

            return new MyCookerPreset.Target(); // ??
        }

        // private
        private static int GetTargetCount()
        {
            var targetCount = 0;

            var files = Directory.GetFiles(CookerTemp);
            foreach (var file in files)
            {
                if (file.Contains("target_"))
                    targetCount++;
            }

            return targetCount;
        }

        // private
        private static void DeleteCurrentTarget()
        {
            var files = Directory.GetFiles(CookerTemp).ToList();

            files.Sort();
            files.Reverse();

            foreach (var file in files)
            {
                if (file.Contains("target_"))
                {
                    File.Delete(file);
                    return;
                }
            }
        }

        // private
        private static void CopyAssemblies()
        {
            var path = AssembliesPath;
            var assembiles = Directory.GetFiles(path, "*.dll");

            var target = GetCurrentTarget();
            var outputPath = BuildPath + target.OutputName + "/" + target.ExecutableName + "_Data/" + "Managed/";

            foreach (var asm in assembiles)
            {
                var fileInfo = new FileInfo(asm);

                // copy file
                File.Copy(asm, outputPath + fileInfo.Name, true);

                Debug.Log(outputPath + fileInfo.Name);
            }
        }

        // private
        private static void CleanTempDir()
        {
            if (Directory.Exists(CookerTemp))
                Directory.Delete(CookerTemp);
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


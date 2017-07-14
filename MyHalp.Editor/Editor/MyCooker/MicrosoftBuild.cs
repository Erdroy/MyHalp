// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MyHalp.Editor.MyCooker
{
    public static class MicrosoftBuild
    {
        // private
        private static string FindMicrosoftSolution()
        {
            var rootPath = Application.dataPath.Replace("Assets", "");
            var dir = new DirectoryInfo(rootPath);
            var solutionFiles = dir.GetFiles("*.csproj");

            if (solutionFiles.Length == 0)
            {
                Debug.LogError("C# Project file not found!");
                return "";
            }
            // TODO: better csproj file search
            return solutionFiles[0].FullName;
        }

        /// <summary>
        /// Compiles C# Project with specified defines.
        /// </summary>
        /// <param name="defines">The defines to be used.</param>
        public static void CompileSolution(string[] defines)
        {
            var solution = FindMicrosoftSolution();

            if (string.IsNullOrEmpty(solution))
                return;

            Debug.Log("Building C# Project file: " + solution);

            // find msbuild
            const string msbuildBase = @"C:\Program Files (x86)\MSBuild";

            if (!Directory.Exists(msbuildBase))
            {
                Debug.Log("MSBuild not found! Cannot script-only compile.");
                return;
            }

            // TODO: vs2017 support

            const string msbuild12 = @"C:\Program Files (x86)\MSBuild\12.0";
            const string msbuild14 = @"C:\Program Files (x86)\MSBuild\14.0";

            var use12 = Directory.Exists(msbuild12);
            var use14 = Directory.Exists(msbuild14);

            var msbuildPath = string.Empty;

            if (use14)
            {
                msbuildPath = msbuild14;
                Debug.Log("Using MSBuild 14.0");
            }
            else if (use12)
            {
                msbuildPath = msbuild12;
                Debug.Log("Using MSBuild 12.0");
            }

            if (string.IsNullOrEmpty(msbuildPath))
            {
                Debug.Log("Any suitable MSBuild version (12.0 or 14.0) not found! Cannot script-only compile.");
                return;
            }

            msbuildPath += @"\Bin\MSBuild.exe";

            // https://msdn.microsoft.com/en-us/library/dd393573.aspx
            // msbuild buildapp.csproj /t:HelloWorld  

            // backup csproj
            File.Copy(solution, solution + ".backup");

            // read solution
            var contents = File.ReadAllText(solution);

            // TODO: change csproj defines

            File.WriteAllText(solution + ".test", contents);

            // TODO: compile

            // resore csproj
            File.Delete(solution);
            File.Move(solution + ".backup", solution);
        }
    }
}

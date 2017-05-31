// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System.IO;
using UnityEngine;

namespace MyHalp.Editor.MyCooker
{
    public static class MicrosoftBuild
    {
        public static void CompileSolution(string solution, string[] defines)
        {
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
                msbuildPath = msbuild14;
            else if (use12)
                msbuildPath = msbuild12;

            if (string.IsNullOrEmpty(msbuildPath))
            {
                Debug.Log("Any suitable MSBuild version (12.0/14.0) not found! Cannot script-only compile.");
                return;
            }

            // TODO: Backup csproj
            // TODO: Change csproj defines

            // TODO: Compile

            // TODO: Resore csproj
        }
    }
}

// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using System.Collections.Generic;
using UnityEditor;

namespace MyHalp.Editor.MyCooker
{
    /// <summary>
    /// MyCooker preset class.
    /// </summary>
    [System.Serializable]
    public class MyCookerPreset
    {
        [System.Serializable]
        public class Target
        {
            public enum BuildType
            {
                Debug,
                Release,
                Shipping
            }

            public string Name;

            public string OutputName;

            public string ExecutableName;

            public bool Headless;

            public BuildTarget BuildTarget;

            public BuildType Type;

            public bool BuildContent;

            public string ContentDirectoryName;

            public BuildAssetBundleOptions ContentBuildOptions;

            public string[] DefineSymbols;

            public string PreBuildAction;

            public string PostBuildAction;

            internal int BuildNumber;
        }

        public string Name;

        public List<Target> Targets;
    }
}

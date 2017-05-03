﻿// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

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

            public string Name { get; set; }

            public string OutputName { get; set; }

            public string ExecutableName { get; set; }

            public bool Headless { get; set; }

            public BuildTarget BuildTarget { get; set; }

            public BuildType Type { get; set; }

            public string[] DefineSymbols { get; set; }

            internal int BuildNumber { get; set; }
        }

        public string Name { get; set; }

        public List<Target> Targets { get; set; }
    }
}
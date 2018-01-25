// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using UnityEditor;

namespace MyHalp.Editor.MyCooker
{
    internal class ScriptsReloader
    {
        public static void ForceReload()
        {
            // I don't know how this works, but it works.
            // I found this solution by a mistake.
            // This file doesn't even exist, 
            // so Unity probably thinks that it was removed.
            AssetDatabase.ImportAsset("Assets/.hidden/RELOADSCRIPTS.cs");
        }
    }
}

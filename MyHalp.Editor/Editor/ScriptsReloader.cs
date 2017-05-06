
using UnityEditor;

namespace MyHalp.Editor
{
    internal class ScriptsReloader
    {
        public static void ForceReload()
        {
            // source: https://forum.unity3d.com/threads/force-script-recompilation.176572/
            AssetDatabase.StartAssetEditing();
            var allAssetPaths = AssetDatabase.GetAllAssetPaths();
            foreach (var assetPath in allAssetPaths)
            {
                if (assetPath.ToLower().Contains("unity"))
                    continue;

                var script = AssetDatabase.LoadAssetAtPath(assetPath, typeof(MonoScript)) as MonoScript;
                if (script != null)
                {
                    AssetDatabase.ImportAsset(assetPath);
                }
            }
            AssetDatabase.StopAssetEditing();
        }
    }
}

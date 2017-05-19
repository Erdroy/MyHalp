
using UnityEditor;

namespace MyHalp.Editor
{
    internal class ScriptsReloader
    {
        public static void ForceReload()
        {
            // reimport MyHalp.Editor.dll to force refresh

            AssetDatabase.StartAssetEditing();
            var asset = AssetDatabase.FindAssets("MyHalp.Editor.dll");
            AssetDatabase.ImportAsset(AssetDatabase.GUIDToAssetPath(asset[0]));
            AssetDatabase.StopAssetEditing();
        }
    }
}

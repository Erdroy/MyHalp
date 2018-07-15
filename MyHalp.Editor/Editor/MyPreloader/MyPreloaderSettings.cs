// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

namespace MyHalp.Editor.MyPreloader
{
    [System.Serializable]
    internal class MyPreloaderSettings
    {
        [System.Serializable]
        public class Asset
        {
            public enum Type
            {
                Unsupported,
                Texture2D,
                RenderTexture,
                GameObject,
                AudioClip,
                Material,
                Shader,
                Mesh,
                AudioMixer,
                ScriptableObject,
                RuntimeAnimatorController
            }

            public Type AssetType;
            public bool IsArray;
            public string Define;
            public string AssetPath;
            public string[] ResourcePath;
            public string FriendlyName;
        }

        public Asset[] Assets;
    }
}

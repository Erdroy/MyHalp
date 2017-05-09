// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

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

            public Type AssetType { get; set; }
            public bool IsArray { get; set; }
            public string Define { get; set; }
            public string AssetPath { get; set; }
            public string[] ResourcePath { get; set; }
            public string FriendlyName { get; set; }
        }

        public Asset[] Assets { get; set; }
    }
}

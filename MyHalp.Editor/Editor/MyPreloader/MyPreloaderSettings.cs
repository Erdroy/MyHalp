// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

namespace MyHalp.Editor.MyPreloader
{
    [System.Serializable]
    internal class MyPreloaderSettings
    {
        public struct Asset
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
                ScriptableObject
            }

            public Type AssetType { get; set; }
            public string AssetPath { get; set; }
            public string ResourcePath { get; set; }
            public string FriendlyName { get; set; }
        }

        public Asset[] Assets { get; set; }
    }
}

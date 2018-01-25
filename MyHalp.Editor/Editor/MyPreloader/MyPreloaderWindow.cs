// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace MyHalp.Editor.MyPreloader
{
    public class MyPreloaderWindow : EditorWindow
    {
        private readonly Dictionary<string, Object> _assetCache = new Dictionary<string, Object>();
        private MyPreloaderSettings _settings;
        private Vector2 _scroll = Vector2.zero;
        private bool _loaded;
        private Object _select;
        private string _friendlyName = "FriendlyName";
        private string _define = "";
        private bool _array = false;

        // private
        private void Init()
        {
            Load();
        }

        // private
        private void Load()
        {
            // load
            if (File.Exists("mypreloader.json"))
            {
                var data = File.ReadAllText("mypreloader.json");
                try
                {
                    _settings = JsonConvert.DeserializeObject<MyPreloaderSettings>(data);
                }
                catch
                {
                    Debug.LogWarning("Failed to load MyPreloader configuration");
                }
            }
            else
            {
                Debug.LogWarning("MyPreloader configuration not found, created new one");
                File.WriteAllText("mypreloader.json", "{}");
                _settings = new MyPreloaderSettings
                {
                    Assets = new MyPreloaderSettings.Asset[] {}
                };
            }
        }

        // private
        private void Save()
        {
            var data = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            File.WriteAllText("mypreloader.json", data); // TODO: check if this wokrks on mac/linux
        }

        // private
        private void Generate()
        {
            var rr = new MyAssetsGenerator
            {
                Session = new Dictionary<string, object>
                {
                    { "Assets", _settings.Assets }
                }
            };
            rr.Initialize();

            var code = rr.TransformText();
            File.WriteAllText("Assets/MyAssets.cs", code);  // TODO: change the path, to be under MyHalp.dll file
            
            // force recompile
            AssetDatabase.ImportAsset("Assets/MyAssets.cs");
        }

        // private
        private void TryAdd()
        {
            // TODO: custom types

            var assetType = MyPreloaderSettings.Asset.Type.Unsupported;

            // validate type
            if (_select is Texture2D)
                assetType = MyPreloaderSettings.Asset.Type.Texture2D;

            else if (_select is RenderTexture)
                assetType = MyPreloaderSettings.Asset.Type.RenderTexture;

            else if (_select is GameObject)
                assetType = MyPreloaderSettings.Asset.Type.GameObject;

            else if (_select is AudioClip)
                assetType = MyPreloaderSettings.Asset.Type.AudioClip;

            else if (_select is Material)
                assetType = MyPreloaderSettings.Asset.Type.Material;

            else if (_select is Shader)
                assetType = MyPreloaderSettings.Asset.Type.Shader;

            else if (_select is Mesh)
                assetType = MyPreloaderSettings.Asset.Type.Mesh;

            else if (_select is AudioMixer)
                assetType = MyPreloaderSettings.Asset.Type.AudioMixer;

            else if (_select is ScriptableObject)
                assetType = MyPreloaderSettings.Asset.Type.ScriptableObject;

            if (_select is RuntimeAnimatorController)
                assetType = MyPreloaderSettings.Asset.Type.RuntimeAnimatorController;

            if (assetType == MyPreloaderSettings.Asset.Type.Unsupported)
            {
                Debug.LogError("MyPreloader: Cannot add new object for preloading, unsupported type.");
                return;
            }

            // validate
            var assetpath = AssetDatabase.GetAssetPath(_select);
            var path = assetpath;
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("MyPreloader: Cannot add new object for preloading, cannot find proper asset path.");
                return;
            }

            // TODO: validate name

            // validate path(do not allow builtin and scene resources)

            var resourcesStart = path.IndexOf("Resources", StringComparison.Ordinal);

            if (!path.StartsWith("Assets/"))
            {
                Debug.LogError("MyPreloader: Cannot add new object for preloading, invalid path.");
                return;
            }
            
            if (resourcesStart <= 0)
            {
                Debug.LogError("MyPreloader: Cannot add new object for preloading, invalid path.");
                return;
            }

            resourcesStart += "Resources/".Length;
            path = path.Substring(resourcesStart, path.Length - resourcesStart);

            var ext = path.LastIndexOf(_array ? "/" : ".", StringComparison.Ordinal);
            path = path.Substring(0, ext);

            var paths = new List<string>();
            if (_array)
            {
                var allAssets = AssetDatabase.FindAssets("t:"+assetType, new []{ Path.GetDirectoryName(assetpath) });

                foreach (var asset in allAssets)
                {
                    path = AssetDatabase.GUIDToAssetPath(asset);
                    
                    resourcesStart = path.IndexOf("Resources", StringComparison.Ordinal);

                    resourcesStart += "Resources/".Length;
                    path = path.Substring(resourcesStart, path.Length - resourcesStart);

                    ext = path.LastIndexOf(".", StringComparison.Ordinal);
                    path = path.Substring(0, ext);

                    paths.Add(path);
                }
            }

            // add
            var assets = new List<MyPreloaderSettings.Asset>(_settings.Assets)
            {
                new MyPreloaderSettings.Asset
                {
                    AssetType = assetType,
                    AssetPath = assetpath,
                    ResourcePath = _array ? paths.ToArray() : new [] {path},
                    IsArray= _array,
                    Define =  _define,
                    FriendlyName = _friendlyName
                }
            };

            Debug.Log("Added new asset for preloading: " + path);

            // set new assets array
            _settings.Assets = assets.ToArray();

            // reset
            _friendlyName = "FriendlyName";
            _select = null;

            // save
            Save();
        }

        // private
        private void OnGUI()
        {
            if (_loaded)
            {
                // skip repaint call to prevent error after window init
                if (Event.current.type == EventType.repaint)
                {
                    _loaded = false;
                    return;
                }
            }

            if (_settings?.Assets == null)
            {
                Load();
                _loaded = true;
                return;
            }

            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal("box", GUILayout.Width(Screen.width - 10.0f));
                {
                    // draw toolbar
                    if (GUILayout.Button("Load", GUILayout.Width(60.0f)))
                    {
                        Load();
                    }
                    if (GUILayout.Button("Save", GUILayout.Width(60.0f)))
                    {
                        Save();
                    }
                    if (GUILayout.Button("Build", GUILayout.Width(60.0f)))
                    {
                        Generate();
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginVertical();
                {
                    _scroll = GUILayout.BeginScrollView(_scroll);
                    {
                        for (var i = 0; i < _settings.Assets.Length; i++)
                        {
                            var asset = _settings.Assets[i];

                            Object cachedAsset;
                            if (!_assetCache.ContainsKey(asset.AssetPath))
                            {
                                cachedAsset = AssetDatabase.LoadAssetAtPath<Object>(asset.AssetPath);

                                // find asset
                                _assetCache.Add(asset.AssetPath, cachedAsset);
                            }
                            else
                            {
                                cachedAsset = _assetCache[asset.AssetPath];
                            }

                            GUILayout.BeginHorizontal("box");
                            {
                                asset.IsArray = GUILayout.Toggle(asset.IsArray, "Array ", GUILayout.Width(60.0f));

                                GUILayout.Label("Object ", GUILayout.Width(60.0f));
                                _assetCache[asset.AssetPath] = EditorGUILayout.ObjectField(cachedAsset, typeof(Object), true);

                                GUILayout.Label("Define ", GUILayout.Width(60.0f));
                                asset.Define = GUILayout.TextField(asset.Define, GUILayout.Width(250.0f));

                                GUILayout.Label("Friendly name ", GUILayout.Width(90.0f));
                                asset.FriendlyName = GUILayout.TextField(asset.FriendlyName, GUILayout.Width(250.0f));

                                if (GUILayout.Button("x", GUILayout.Width(25.0f)))
                                {
                                    // delete the asset
                                    var list = new List<MyPreloaderSettings.Asset>(_settings.Assets);
                                    list.RemoveAt(i);
                                    _settings.Assets = list.ToArray();
                                    GUI.FocusControl(null);
                                    return;
                                }
                            }
                            GUILayout.EndHorizontal();
                        }

                        EditorGUILayout.Space();
                        GUILayout.Label("Add new ", GUILayout.Width(60.0f));

                        GUILayout.BeginHorizontal();
                        {
                            _array = GUILayout.Toggle(_array, "Array ", GUILayout.Width(60.0f));

                            GUILayout.Label("Object ", GUILayout.Width(60.0f));
                            _select = EditorGUILayout.ObjectField(_select, typeof(Object), true);

                            GUILayout.Label("Define ", GUILayout.Width(60.0f));
                            _define = EditorGUILayout.TextField(_define, GUILayout.Width(250.0f));

                            GUILayout.Label("Friendly name ", GUILayout.Width(90.0f));
                            _friendlyName = EditorGUILayout.TextField(_friendlyName, GUILayout.Width(250.0f));

                            if (GUILayout.Button("Add", GUILayout.Width(60.0f)))
                            {
                                // try add new
                                TryAdd();
                            }
                        }
                        GUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }
                    GUILayout.EndScrollView();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Opens the MyCooker window
        /// </summary>
        [MenuItem("Window/MyPreloader")]
        public static void ShowWindow()
        {
            // TODO: Use any editor buildin icon.

            var window = GetWindow<MyPreloaderWindow>();
            window.titleContent.text = "MyPreloader";
            window.autoRepaintOnSceneChange = true;
            window.minSize = new Vector2(800, 300);

            window.Init();
        }
    }
}

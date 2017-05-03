// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyHalp.Editor.MyCooker
{
    /// <summary>
    /// MyCooker main window class.
    /// </summary>
    public class MyCookerWindow : EditorWindow
    {
        private bool _loaded;
        private MyCookerManager _cookerManager;
        private Vector2 _presetScroll = Vector2.zero;
        private Vector2 _platformScroll = Vector2.zero;
        private Vector2 _editorScroll = Vector2.zero;

        private MyCookerNameReader _nameReader;
        
        // private
        private void Init()
        {
            _cookerManager = new MyCookerManager();
            _cookerManager.Load();
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
            if (_cookerManager == null)
            {
                Init();
                _loaded = true;
                return;
            }

            if (_nameReader != null)
            {
                _nameReader.Draw();
                return;
            }

            // draw presets header
            GUILayout.BeginArea(new Rect(0, 0, 180.0f, 50.0f), "", "box");
            {
                DrawHeader();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(0, 50.0f, 180.0f, Screen.height - 50.0f - 15.0f/* the padding: 15px */), "", "box");
            {
                // draw presets
                DrawPresets();
            }
            GUILayout.EndArea();

            // draw selected preset window
            if (_cookerManager.SelectedPreset != null)
            {
                DrawPresetEditorBar();
            }
            else
            {
                // draw 'Please select a preset.'
                GUI.Label(new Rect(Screen.width / 2.0f - 100.0f, Screen.height / 2.0f - 10.0f, 200.0f, 20.0f), "Please select a preset.");
            }
        }
        
        // private
        private void DrawNameReader()
        {
            
        }

        // private
        private void DrawHeader()
        {
            GUILayout.Label("Presets");
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("new preset", GUILayout.Height(20.0f)))
                {
                    _nameReader = new MyCookerNameReader("Create preset", readName =>
                    {
                        _cookerManager.Presets.Add(new MyCookerPreset
                        {
                            Name = readName,
                            Targets = new List<MyCookerPreset.Target>()
                        });
                        _cookerManager.SelectedPreset = _cookerManager.Presets[_cookerManager.Presets.Count - 1];
                        _cookerManager.Save();
                        _nameReader = null;
                        GUI.FocusControl(null);
                    }, 
                    delegate {
                        _nameReader = null;
                        GUI.FocusControl(null);
                    });
                }
            }
            GUILayout.EndHorizontal();
        }

        // private
        private void DrawPresets()
        {
            _presetScroll = GUILayout.BeginScrollView(_presetScroll);
            foreach (var preset in _cookerManager.Presets)
            {
                // draw preset
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(preset.Name, GUILayout.Height(25.0f)))
                {
                    // select preset
                    _cookerManager.SelectedPreset = preset;
                    _cookerManager.SelectedTarget = null;
                    _cookerManager.Save();
                    GUI.FocusControl(null);
                }

                // remove button
                if (GUILayout.Button("x", GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
                {
                    // remove preset
                    _cookerManager.Presets.Remove(preset);
                    _cookerManager.SelectedPreset = null;
                    _cookerManager.SelectedTarget = null;
                    _cookerManager.Save();
                    GUI.FocusControl(null);
                    return;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }

        // private
        private void DrawPresetEditorBar()
        {
            var targetsWidth = 220.0f;
            GUILayout.BeginArea(new Rect(180.0f, 0.0f, targetsWidth, 50.0f), "", "box");
            {
                GUILayout.BeginVertical(GUILayout.Height(45.0f));
                {
                    GUILayout.Label("Targets");
                    if (GUILayout.Button("add target", GUILayout.Height(20.0f)))
                    {
                        _nameReader = new MyCookerNameReader("Create target", readName =>
                        {
                            // add target
                            _cookerManager.SelectedPreset.Targets.Add(new MyCookerPreset.Target
                            {
                                Name = readName,
                                BuildTarget = BuildTarget.StandaloneWindows64,
                                OutputName = "game_win_64",
                                ExecutableName = "game_win_64",
                                DefineSymbols = new string[] {}
                            });
                            _cookerManager.Save();
                            _nameReader = null;
                            GUI.FocusControl(null);
                        }, 
                        delegate {
                            _nameReader = null;
                            GUI.FocusControl(null);
                        });
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(180.0f, 50.0f, targetsWidth, Screen.height - 50.0f - 15.0f/* the padding: 15px */), "", "box");
            {
                GUILayout.BeginVertical(GUILayout.Width(targetsWidth));
                {
                    _platformScroll = GUILayout.BeginScrollView(_platformScroll);
                    foreach (var target in _cookerManager.SelectedPreset.Targets)
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(target.Name, GUILayout.Height(25.0f)))
                        {
                            // select preset
                            _cookerManager.SelectedTarget = target;
                            GUI.FocusControl(null);
                        }

                        // remove button
                        if (GUILayout.Button("x", GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
                        {
                            // remove preset
                            if(target == _cookerManager.SelectedTarget)
                                _cookerManager.SelectedTarget = null;

                            _cookerManager.SelectedPreset.Targets.Remove(target);
                            _cookerManager.Save();
                            GUI.FocusControl(null);
                            return;
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndScrollView();
                }
                GUILayout.EndVertical();
                if (GUILayout.Button("BUILD(ALT+B)", GUILayout.Height(30.0f)))
                {
                    
                }
                if (GUILayout.Button("BUILD SCRIPTS(ALT+SHIFT+B)", GUILayout.Height(30.0f)))
                {

                }
                GUILayout.Space(10.0f);
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(180.0f + targetsWidth, 0.0f, Screen.width - (180.0f + targetsWidth), Screen.height));
            {
                if (_cookerManager.SelectedTarget == null)
                {
                    // draw 'Please select a target.'
                    GUI.Label(new Rect(Screen.width/2.0f - 100.0f, Screen.height/2.0f - 10.0f, 200.0f, 20.0f),
                        "Please select a target.");
                }
                else
                {
                    DrawPresetEditor();
                }
            }
            GUILayout.EndArea();
        }

        // private
        private void DrawPresetEditor()
        {
            _editorScroll = GUILayout.BeginScrollView(_editorScroll);
            {
                GUILayout.BeginHorizontal("box");
                {
                    if (GUILayout.Button("help", GUILayout.Width(70.0f)))
                    {
                        // TODO: Start documentation using something like: Process.Start("http://myhalp.github.io/docs/mycooker");
                    }
                    if (GUILayout.Button("save", GUILayout.Width(70.0f)))
                    {
                        _cookerManager.Save();
                    }
                    if (GUILayout.Button("rename", GUILayout.Width(70.0f)))
                    {
                        _nameReader = new MyCookerNameReader("Change target name", readName =>
                        {
                            GUI.FocusControl(null);
                            _cookerManager.SelectedTarget.Name = readName;
                            _cookerManager.Save();
                            _nameReader = null;
                        },
                       delegate
                       {
                           _nameReader = null;
                       });
                    }
                }
                GUILayout.EndHorizontal();

                var target = _cookerManager.SelectedTarget;

                // basic settings
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                GUILayout.Label("Target configuration (" + target.Name + ")");
                EditorGUILayout.Space();

                target.ExecutableName = EditorGUILayout.TextField("Executable file name", target.ExecutableName);
                target.OutputName = EditorGUILayout.TextField("Output folder name", target.OutputName);

                if (target.Type != MyCookerPreset.Target.BuildType.Debug)
                    target.Headless = EditorGUILayout.Toggle("Headless", target.Headless);
                target.Type = (MyCookerPreset.Target.BuildType)EditorGUILayout.EnumPopup("Build type", target.Type);
                target.BuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("Build target", target.BuildTarget);


                // build directives
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                GUILayout.Label("Scripting define symbols");
                EditorGUILayout.Space();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("add define", GUILayout.Width(150.0f)))
                {
                    // add new define

                    // check if directive array is null, if so, create new one with one directive
                    if (target.DefineSymbols == null)
                        target.DefineSymbols = new[] {""};
                    else
                    {
                        var list = new List<string>(target.DefineSymbols)
                        {
                            "NEW_DIRECTIVE"
                        };
                        target.DefineSymbols = list.ToArray();
                    }
                }
                if (GUILayout.Button("see builtin symbols", GUILayout.Width(150.0f)))
                {
                    // TODO: start docs
                }
                GUILayout.EndHorizontal();

                EditorGUILayout.Space();
                if (target.DefineSymbols != null)
                {
                    for (var i = 0; i < target.DefineSymbols.Length; i++)
                    {
                        GUILayout.BeginHorizontal();
                        target.DefineSymbols[i] = GUILayout.TextField(target.DefineSymbols[i]);
                        if (GUILayout.Button("x", GUILayout.Width(25.0f)))
                        {
                            // safe delete directive
                            var list = new List<string>(target.DefineSymbols);
                            list.RemoveAt(i);
                            target.DefineSymbols = list.ToArray();
                            return;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndScrollView();

        }

        /// <summary>
        /// Opens the MyCooker window
        /// </summary>
        [MenuItem("Window/MyCooker/Open Window")]
        public static void ShowWindow()
        {
            // TODO: Use any editor buildin icon.

            var window = GetWindow<MyCookerWindow>();
            window.titleContent.text = "MyCooker";
            window.autoRepaintOnSceneChange = true;
            window.minSize = new Vector2(800, 300);

            window.Init();
        }

        /// <summary>
        /// Stats preset build
        /// </summary>
        [MenuItem("Window/MyCooker/Build &B")]
        public static void Build()
        {
            var cookerManager = new MyCookerManager();
            cookerManager.Load();

            cookerManager.Build();
        }

        /// <summary>
        /// Stats preset scripts only build
        /// </summary>
        [MenuItem("Window/MyCooker/Build Scripts only &#B")]
        public static void BuildScripts()
        {
            var cookerManager = new MyCookerManager();
            cookerManager.Load();

            cookerManager.BuildScripts();
        }
    }
}
// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

#define USE_MYLOGGER

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyConsole class, allows to show console window.
    /// </summary>
    public static class MyConsole
    {
        // private
        [UsedImplicitly]
        private class MyConsoleWindow : MyComponent
        {
            private readonly List<string> _messages = new List<string>();
            private readonly List<string> _drawmessages = new List<string>();
            private readonly List<string> _commands = new List<string>();
            private int _backCommand;
            private bool _enabled;
            private bool _forceFocus;
            private string _currentCommand = "";
            private Vector2 _scrollPos;
            private Texture2D _backgroundTexture;

            private GUIStyle _styleBox;
            private GUIStyle _styleField;
            
            private void Start()
            {
                // TODO: gui style

                _backgroundTexture = new Texture2D(16, 16, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Point
                };

                var colors = new Color[16*16];
                for (var i = 0; i < 16*16; i++)
                {
                    colors[i] = new Color(0.1f, 0.1f, 0.1f, 0.55f);
                }

                _backgroundTexture.SetPixels(colors);
                _backgroundTexture.Apply(false, true);

                _styleBox = new GUIStyle
                {
                    normal = new GUIStyleState
                    {
                        background = _backgroundTexture,
                        textColor = Color.white
                    },
                    border = new RectOffset(0, 0, 0, 0),
                    richText = true,
                    alignment = TextAnchor.MiddleCenter
                };

                _styleField = new GUIStyle
                {
                    normal = new GUIStyleState
                    {
                        background = _backgroundTexture,
                        textColor = Color.white
                    },
                    border = new RectOffset(5, 5, 0, 0),
                    padding = new RectOffset(5, 5, 0, 0),
                    richText = true,
                    alignment = TextAnchor.MiddleLeft
                };
                
                _scrollPos = Vector2.zero;
            }
            
            private void Update()
            {
                // toggle enable
                if (Input.GetKeyDown(MySettings.ConsoleOpenCloseKey))
                {
                    _enabled = !_enabled;
                    _forceFocus = true;
                }
            }

            // internal
            internal void AddMessage(string message, MyLoggerLevel level)
            {
                if(_messages.Count >= MySettings.MaxConsoleMessages)
                    _messages.RemoveAt(0);

                // remove new line from the end
                if (message[message.Length - 1] == '\n')
                    message = message.Remove(message.Length - 1, 1);

                // set message color
                switch (level)
                {
                    case MyLoggerLevel.Info:
                    case MyLoggerLevel.Debug:
                        break;
                    case MyLoggerLevel.Warning:
                        message = "<color=yellow>" + message + "</color>";
                        break;
                    case MyLoggerLevel.Error:
                        message = "<color=red><b>" + message + "</b></color>";
                        break;
                    case MyLoggerLevel.Fatal:
                        message = "<color=red><b><i>" + message + "</i></b></color>";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(level), level, null);
                }

                // add message
                lock (_messages) _messages.Add(message);

                // scroll to the down of scroll view
                _scrollPos.y = float.MaxValue;
            }

            // internal
            internal void BindLogger()
            {
#if USE_MYLOGGER
                // bind logger
                MyLogger.OnMessage += AddMessage;
#else
                // TODO: Handle Unity logs in console without MyLogger
#endif
            }

            // internal
            internal void ClearInternal()
            {
                _messages.Clear();
            }
            
            // private
            private void OnGUI()
            {
                if (!_enabled)
                    return;
                
                var screenW = Screen.width;
                var screenH = Screen.height;

                // console window
                GUILayout.BeginArea(new Rect(0.0f, 0.0f, screenW, screenH));
                {
                    GUILayout.BeginVertical();
                    {
                        // message view
                        GUILayout.BeginVertical(_styleBox, GUILayout.Width(screenW), GUILayout.Height(screenH - 25.0f));
                        {
                            // make scroll bar
                            _scrollPos = GUILayout.BeginScrollView(_scrollPos);
                            {
                                // show all messages
                                if (Event.current.type == EventType.Layout)
                                {
                                    lock (_messages)
                                    {
                                        _drawmessages.Clear();
                                        _drawmessages.AddRange(_messages);
                                    }
                                }

                                foreach (var message in _drawmessages)
                                {
                                    GUILayout.Label(message, GUILayout.MaxWidth(screenW));
                                }
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndVertical();

                        // view the input field and ok button 
                        // only when commands are initialized
                        if (MyCommands.Instance != null)
                        {
                            GUILayout.BeginHorizontal(_styleBox, GUILayout.Height(25.0f));
                            {
                                GUILayout.Space(5.0f);

                                if (Event.current.isKey && Event.current.type == EventType.KeyDown &&
                                    (Event.current.keyCode == MySettings.ConsoleOpenCloseKey || Event.current.keyCode == KeyCode.Escape))
                                {
                                    _enabled = false;
                                }

                                // update the hints
                                UpdateHints();

                                GUI.SetNextControlName("command_input");
                                _currentCommand = GUILayout.TextField(_currentCommand, _styleField, GUILayout.Width(screenW - 95.0f), GUILayout.Height(20.0f));

                                GUILayout.Space(5.0f);

                                if (GUILayout.Button("ok", _styleBox, GUILayout.Width(75.0f), GUILayout.Height(20.0f)) || (Event.current.isKey && Event.current.keyCode == KeyCode.Return))
                                {
                                    if (!string.IsNullOrEmpty(_currentCommand))
                                    {
                                        _commands.Add(_currentCommand);
                                        _backCommand = _commands.Count;
                                        AddMessage(_currentCommand, MyLoggerLevel.Info);
                                        MyCommands.Instance.ExecuteRaw(_currentCommand);
                                        _currentCommand = string.Empty;
                                    }
                                }
                            }
                            GUILayout.EndHorizontal();

                            // draw hints
                            DrawHints();
                        }
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndArea();

                if (_forceFocus)
                {
                    // focus `command_input` text field if the console was opened in this frame
                    GUI.FocusControl("command_input");
                    _forceFocus = false;
                }
            }

            private void DrawHints()
            {
                if (_currentCommand.Length > 0)
                {
                    // find and show similar commands
                    GUILayout.BeginArea(new Rect(5.0f, Screen.height - 200.0f, Screen.width - 200.0f, 175.0f), _styleBox);
                    {
                        // find and show similar commands
                        var commands = MyCommands.Instance.GetAllCommands();
                        var hints = commands.Where(x => x.Name.StartsWith(_currentCommand));

                        foreach (var hint in hints)
                        {
                            var parameters = "";

                            foreach (var parameter in hint.Parameters)
                            {
                                parameters += " " + parameter.Name;
                                switch (parameter.ParameterType.Name.ToLower())
                                {
                                    case "string":
                                        parameters += "[string]";
                                        break;
                                    case "int32":
                                        parameters += "[integer]";
                                        break;
                                    case "single":
                                    case "double":
                                        parameters += "[float]";
                                        break;
                                    case "boolean":
                                        parameters += "[boolean]";
                                        break;
                                }
                            }

                            GUILayout.Label(hint.Name + " " + parameters + " - " + hint.Description);
                        }
                    }
                    GUILayout.EndArea();
                }
            }

            // private
            private void UpdateHints()
            {
                if (_currentCommand?.Length > 0)
                {
                    if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Tab)
                    {
                        var commands = MyCommands.Instance.GetAllCommands();
                        var command = commands.FirstOrDefault(x => x.Name.StartsWith(_currentCommand));

                        if(!string.IsNullOrEmpty(command.Name))
                            _currentCommand = command.Name;

                        var textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                        textEditor.MoveTextEnd();
                    }
                }

                if (_commands.Count > 0)
                {
                    if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
                    {
                        _backCommand--;
                        _backCommand = Mathf.Clamp(_backCommand, 0, _commands.Count - 1);
                        _currentCommand = _commands[_backCommand];

                        var textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                        textEditor.MoveTextEnd();
                    }
                    if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
                    {
                        _backCommand++;
                        _backCommand = Mathf.Clamp(_backCommand, 0, _commands.Count - 1);
                        _currentCommand = _commands[_backCommand];

                        var textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                        textEditor.MoveTextEnd();
                    }
                }
            }
        }

        private static MyConsoleWindow _consoleWindow;

        /// <summary>
        /// Initializes the console window.
        /// </summary>
        /// <param name="bindLogger">Flag to allow logger binding or not(Show unity/logger logs). 
        /// To use this you will need also enabled this settings: 
        /// MySettings.UseLogCallback = true; 
        /// and 
        /// MySettings.UseDispatchedLogCallback = true;
        /// </param>
        public static void Init(bool bindLogger = true)
        {
            if (_consoleWindow != null)
            {
                Debug.LogError("Console window can be initialized only once!");
                return;
            }

            // create instance of console window
            _consoleWindow = MyInstancer.Create<MyConsoleWindow>();
            
            if (bindLogger)
            {
                // bind logger if not disabled.
                _consoleWindow.BindLogger();
            }

            // register default commands
            MyCommands.Register("", "clear", Clear, "Clears all console messages.");

            MyCommands.Register("", "list", delegate
            {
                var commands = MyCommands.Instance.GetAllCommands();

                foreach (var command in commands)
                {
                    var parameters = "";

                    foreach (var parameter in command.Parameters)
                    {
                        parameters += " " + parameter.Name;
                        switch (parameter.ParameterType.Name.ToLower())
                        {
                            case "string":
                                parameters += "[string]";
                                break;
                            case "int32":
                                parameters += "[integer]";
                                break;
                            case "single":
                            case "double":
                                parameters += "[float]";
                                break;
                            case "boolean":
                                parameters += "[boolean]";
                                break;
                        }
                    }

                    Write(command.Name + " " + parameters + " - " + command.Description);
                }
            }, "Lists all commands.");
        }

        /// <summary>
        /// Clears all console messages.
        /// </summary>
        public static void Clear()
        {
            _consoleWindow.ClearInternal();
        }

        /// <summary>
        /// Writes to the console window.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Write(string message)
        {

            if (_consoleWindow == null)
            {
                Debug.LogError("Cannot write to the console, console is not initialized!");
                return;
            }

            // write
            _consoleWindow.AddMessage(message, MyLoggerLevel.Debug);
        }
    }
}
// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
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
            private bool _enabled;
            private bool _forceFocus;
            private string _currentCommand = "";
            private Vector2 _scrollPos;

            // override `OnInit`
            protected override void OnInit()
            {
                // TODO: gui style

                _scrollPos = Vector2.zero;
            }

            // override `OnTick`
            protected override void OnTick()
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
            }

            // internal
            internal void BindLogger()
            {
                // bind logger
                MyLogger.OnMessage += AddMessage;
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
                    GUILayout.BeginVertical(GUILayout.Width(800.0f), GUILayout.Height(350.0f));
                    {
                        // message view
                        GUILayout.BeginVertical("box", GUILayout.Width(screenW - 10.0f), GUILayout.Height(screenH - 35.0f));
                        {
                            // make scroll bar
                            _scrollPos = GUILayout.BeginScrollView(_scrollPos);
                            {
                                // show all messages
                                if (Event.current.type == EventType.layout)
                                {
                                    lock (_messages)
                                    {
                                        _drawmessages.Clear();
                                        _drawmessages.AddRange(_messages);
                                    }
                                }

                                foreach (var message in _drawmessages)
                                {
                                    GUILayout.Label(message, GUILayout.MaxWidth(screenW - 20.0f), GUILayout.MinHeight(20.0f));
                                }
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndVertical();

                        // view the input field and ok button
                        GUILayout.BeginHorizontal("box");
                        {
                            GUI.SetNextControlName("command_input");
                            _currentCommand = GUILayout.TextField(_currentCommand, GUILayout.Width(screenW - 110.0f));

                            if (GUILayout.Button("ok", GUILayout.Width(90.0f)) ||
                                (Event.current.isKey && Event.current.keyCode == KeyCode.Return))
                            {
                                AddMessage(_currentCommand, MyLoggerLevel.Info);
                                _currentCommand = string.Empty;

                                // TODO: execute command
                            }
                        }
                        GUILayout.EndHorizontal();
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
        }

        private static MyConsoleWindow _consoleWindow;

        /// <summary>
        /// Initializes the console window.
        /// </summary>
        /// <param name="bindLogger">Flag to allow logger binding or not(Show unity/logger logs). 
        /// To use this you will need also enabled this settings: 
        /// MySettings.UseLogCallback = true; 
        /// and 
        /// MySettings.UseDispathedLogCallback = true;
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
// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace MyHalp
{
    public delegate void MyMessageHandler(string message);
    
    /// <summary>
    /// MyLogger class - allows to produce log file.
    /// It can be used for some debugging etc.
    /// </summary>
    public static class MyLogger
    {
        private static bool _initialized;

        public static event MyMessageHandler OnMessage;

        private static void UnityHandle(string condition, string stackTrace, LogType type)
        {
            if (!MySettings.ProduceLogFile || !_initialized)
                return;

            Add(condition, stackTrace);
        }

        /// <summary>
        /// Begins new MyLogger session.
        /// </summary>
        public static void Begin()
        {
            if (_initialized)
            {
                // Throw error, because there can be only one MyLogger session.
                throw new UnityException("Cannot begin new MyLogger session, " +
                                         "because last session still exists, " +
                                         "call MyLogger.End() to allow this.");
            }

            _initialized = true;

            // Add the unity3d log message handle
            Application.logMessageReceived += UnityHandle;

            // Remove old file or backup it
            if (MySettings.BackupOldLogs)
            {
                // Backup old log file

                if (!File.Exists(MySettings.LogFile))
                    return;

                if (!Directory.Exists(MySettings.BackupFolder))
                    Directory.CreateDirectory(MySettings.BackupFolder);

                File.Move(MySettings.LogFile, MySettings.BackupFolder +"/" + MySettings.LogFile.Split('.')[0] + DateTime.Now.ToString("HH-mm-ss") + ".txt");
            }
            else
            {
                // Remove old file
                if (File.Exists(MySettings.LogFile))
                    File.Delete(MySettings.LogFile);
            }
        }

        /// <summary>
        /// Produce stack point in the log file.
        /// Something like: "MyNameSpace.MyClass::MyMethod at 160"
        /// </summary>
        public static void Point()
        {
            if (!MySettings.ProduceLogFile || !_initialized)
                return;
            
            var callStack = new StackFrame(1, true);
            var callstackString = callStack.GetMethod().ReflectedType + "." + 
                                    callStack.GetMethod().Name + " at " +
                                    callStack.GetFileLineNumber();

            callstackString = callstackString.Replace(".", MySettings.PointSeparator);
            Add(callstackString);
        }

        /// <summary>
        /// Add new message.
        /// </summary>
        /// <param name="message">The message</param>
        public static void Add(string message)
        {
            if (!MySettings.ProduceLogFile || !_initialized)
                return;

            Add(message, "");
        }

        /// <summary>
        /// Add new message with sender object.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="sender">The sender</param>
        public static void Add(string message, object sender)
        {
            if (!MySettings.ProduceLogFile || !_initialized)
                return;

            // Construct log message
            var msg = string.Format("{0} {1}: {2}", DateTime.Now.ToString(MySettings.TimeFormat), sender, message);
            msg = msg.Replace("\n", "") + "\n";

            File.AppendAllText(MySettings.LogFile, msg);

            if (OnMessage != null)
                OnMessage(msg);
        }

        /// <summary>
        /// Close the current session.
        /// </summary>
        public static void End()
        {
            // Remove the unity3d log message handle
            Application.logMessageReceived -= UnityHandle;
            _initialized = false;
        }
    }
}
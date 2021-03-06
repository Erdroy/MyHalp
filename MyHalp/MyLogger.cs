﻿// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEngine;

namespace MyHalp
{
    public delegate void MyMessageHandler(string message, MyLoggerLevel level);

    /// <summary>
    /// MyLogger class - allows to produce log file.
    /// It can be used for some debugging etc.
    /// </summary>
    public class MyLogger : IDisposable
    {
        private struct Log
        {
            public string Message { get; set; }
            public object Sender { get; set; }
            public MyLoggerLevel Level { get; set; }
            public DateTime Time { get; set; }
        }

        private static MyLogger _instance;

        private bool _disposed;
        private Thread _logThread;
        private FileStream _logStream;
        private StreamWriter _logWriter;
        private Queue<Log> _logQueue;

        // private
        private void Setup() // TODO: change name to better name - Init, when static Init is removed
        {
            // create log queue
            _logQueue = new Queue<Log>();
            
            // Add the unity3d log message handle
            Application.logMessageReceived += UnityHandle;
            
            // create log thread
            _logThread = new Thread(ThreadRunner);
            _logThread.Start();

            // remove old file or backup it - if it exists
            if (MySettings.BackupOldLogs && File.Exists(MySettings.LogFile))
            {
                // backup old log file
                
                // create backup directory if doesn't exist yet.
                if (!Directory.Exists(MySettings.BackupFolder))
                    Directory.CreateDirectory(MySettings.BackupFolder);

                // TODO: compress file

                // move the file and change it's name
                File.Copy(MySettings.LogFile, MySettings.BackupFolder + "/" + MySettings.LogFile.Split('.')[0] + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".txt");
            }
            else
            {
                // Remove old file
                if (File.Exists(MySettings.LogFile))
                    File.Delete(MySettings.LogFile);
            }

            // create log file stream
            _logStream = new FileStream(MySettings.LogFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            _logWriter = new StreamWriter(_logStream);
        }

        // private
        private void ThreadRunner()
        {
            while (!_disposed)
            {
                while (_logQueue.Count > 0) // process all logs
                {
                    // dequeue next log
                    var log = _logQueue.Dequeue();

                    // construct log message
                    var message = ConstructMessage(log);

                    // write log to the file
                    _logWriter.Write(message);

                    if (MySettings.UseLogCallback && !MySettings.UseDispatchedLogCallback)
                    {
                        // try call OnMessage
                        if (OnMessage != null)
                            OnMessage(message, log.Level);
                    }

                    // flush
                    _logStream.Flush();
                    _logWriter.Flush();
                }

                Thread.Sleep(MySettings.LoggerThreadFrequency); // sleep some time to get some more new fresh logs to eat.
            }
        }

        // private
        private void UnityHandle(string condition, string stackTrace, LogType type)
        {
            if (!MySettings.ProduceLogFile)
                return;

            MyLoggerLevel logType;
            switch (type)
            {
                case LogType.Log:
                    logType = MyLoggerLevel.Info;
                    break;
                case LogType.Assert:
                    logType = MyLoggerLevel.Debug;
                    break;
                case LogType.Warning:
                    logType = MyLoggerLevel.Warning;
                    break;
                case LogType.Error:
                    logType = MyLoggerLevel.Error;
                    break;
                case LogType.Exception:
                    logType = MyLoggerLevel.Fatal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            Write(condition, stackTrace, logType);
        }

        // private
        private string ConstructMessage(Log log)
        {
            // construct log message
            var msg = log.Sender != null ?
                string.Format("{0} [{1}] {2}: {3}", log.Time.ToString(MySettings.TimeFormat), log.Level, log.Sender, log.Message) :
                string.Format("{0} [{1}] {2}", log.Time.ToString(MySettings.TimeFormat), log.Level, log.Message);
            
            return msg.Replace("\n", "") + "\n";
        }

        /// <summary>
        /// Write the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sender">The message sender(optional, use null when don't want to use this)</param>
        /// <param name="level">The log level.</param>
        public void Write(string message, object sender, MyLoggerLevel level)
        {
            if (!MySettings.ProduceLogFile)
                return;

            // construct log
            var log = new Log
            {
                Level = level,
                Message = message,
                Sender = sender,
                Time = DateTime.Now
            };

            lock (_logQueue)
            {
                // enqueue the log message
                _logQueue.Enqueue(log);
            }
            
            if (MySettings.UseLogCallback && MySettings.UseDispatchedLogCallback)
            {
                // try call OnMessage
                if (OnMessage != null)
                    OnMessage(ConstructMessage(log), log.Level);
            }
        }

        /// <summary>
        /// Dispose logger.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            _logStream.Dispose();
            _logWriter.Dispose();
            _logQueue.Clear();
            _logThread.Abort();            
            _instance = null;
        }

        [Obsolete("This is not needed anymore, will be removed in future release.")]
        public static void Init()
        {
        }

        /// <summary>
        /// Produce stack point in the log output.
        /// Something like: "MyNameSpace.MyClass::MyMethod at 160"
        /// </summary>
        public static void Point()
        {
            if (!MySettings.ProduceLogFile)
                return;
            
            var callStack = new StackFrame(1, true);
            var callstackString = callStack.GetMethod().ReflectedType + "." +
                                    callStack.GetMethod().Name + " at " +
                                    callStack.GetFileLineNumber();

            callstackString = callstackString.Replace(".", MySettings.PointSeparator);
            Instance.Write(callstackString, null, MyLoggerLevel.Debug);
        }

        /// <summary>
        /// Produce stack trace in the log output.
        /// </summary>
        public static void Trace()
        {
            var trace = new StackTrace();
            Add(trace.ToString(), MyLoggerLevel.Debug);
        }

        /// <summary>
        /// Add new message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="logLevel">The log level.</param>
        public static void Add(string message, MyLoggerLevel logLevel = MyLoggerLevel.Info)
        {
            if (!MySettings.ProduceLogFile)
                return;

            Instance.Write(message, null, logLevel);
        }

        /// <summary>
        /// Add new message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="p1">{0}</param>
        /// <param name="logLevel">The log level.</param>
        public static void Add(string message, object p1, MyLoggerLevel logLevel = MyLoggerLevel.Info)
        {
            Add(string.Format(message, p1), logLevel);
        }

        /// <summary>
        /// Add new message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="p1">{0}</param>
        /// <param name="p2">{1}</param>
        /// <param name="logLevel">The log level.</param>
        public static void Add(string message, object p1, object p2, MyLoggerLevel logLevel = MyLoggerLevel.Info)
        {
            Add(string.Format(message, p1, p2), logLevel);
        }

        /// <summary>
        /// Add new message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="p1">{0}</param>
        /// <param name="p2">{1}</param>
        /// <param name="p3">{2}</param>
        /// <param name="logLevel">The log level.</param>
        public static void Add(string message, object p1, object p2, object p3, MyLoggerLevel logLevel = MyLoggerLevel.Info)
        {
            Add(string.Format(message, p1, p2, p3), logLevel);
        }

        /// <summary>
        /// Add new message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="p1">{0}</param>
        /// <param name="p2">{1}</param>
        /// <param name="p3">{2}</param>
        /// <param name="p4">{3}</param>
        /// <param name="logLevel">The log level.</param>
        public static void Add(string message, object p1, object p2, object p3, object p4, MyLoggerLevel logLevel = MyLoggerLevel.Info)
        {
            Add(string.Format(message, p1, p2, p3, p4), logLevel);
        }

        /// <summary>
        /// Add new message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="p1">{0}</param>
        /// <param name="p2">{1}</param>
        /// <param name="p3">{2}</param>
        /// <param name="p4">{3}</param>
        /// <param name="p5">{4}</param>
        /// <param name="logLevel">The log level.</param>
        public static void Add(string message, object p1, object p2, object p3, object p4, object p5, MyLoggerLevel logLevel = MyLoggerLevel.Info)
        {
            Add(string.Format(message, p1, p2, p3, p4, p5), logLevel);
        }

        /// <summary>
        /// MyLogger current instance.
        /// </summary>
        public static MyLogger Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                // create new instance of logger
                _instance = new MyLogger();
                _instance.Setup();

                return _instance;
            }
        }

        /// <summary>
        /// OnMessage event - called when new log is queued.
        /// MySettings.UseLogCallback = true; is required!
        /// </summary>
        public static event MyMessageHandler OnMessage;
    }
}
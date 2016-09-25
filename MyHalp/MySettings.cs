// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System.Threading;

namespace MyHalp
{
    public static class MySettings
    {
        // MyLogger settings

        /// <summary>
        /// Can MyLogger save copy old log files into ./logs/?
        /// </summary>
        public static bool BackupOldLogs = true;

        /// <summary>
        /// The MyLogger output file.
        /// </summary>
        public static string LogFile = "log.txt";

        /// <summary>
        /// The MyLogger backup folder.
        /// </summary>
        public static string BackupFolder = "logs";

        /// <summary>
        /// The point separator,
        /// Used like: MyNamespace::MyClass::MyMethod
        /// </summary>
        public static string PointSeparator = "::";

        /// <summary>
        /// The MyLogger time format.
        /// </summary>
        public static string TimeFormat = "dd/MM/yyyy HH:mm:ss";

        /// <summary>
        /// How frequently the logger thread(if enabled) is saving logs.
        /// </summary>
        public static int LoggerThreadFrequency = 10;

        /// <summary>
        /// Can MyLogger produce log file?
        /// </summary>
        public static bool ProduceLogFile = true;

        /// <summary>
        /// Use new thread for saving logs?
        /// Should be set before calling `MyLogger.Begin()`.
        /// </summary>
        public static bool LoggerThread = true;

        /// <summary>
        /// Gets or sets the maximum number of threads used by MyJob.
        /// </summary>
        public static int MaxThreads
        {
            get
            {
                int count0;
                int count1;
                ThreadPool.GetMaxThreads(out count0, out count1);
                return count0;
            }
            set { ThreadPool.SetMaxThreads(value, value); }
        }
    }
}
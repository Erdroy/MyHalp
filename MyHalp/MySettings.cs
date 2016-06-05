// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

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
        /// Can MyLogger produce log file?
        /// </summary>
        public static bool ProduceLogFile = true;
    }
}
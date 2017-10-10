// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// Unity Debug log helper class.
    /// </summary>
    internal static class UnityLog
    {
        public static void Log(string message, MyLoggerLevel level)
        {
            switch (level)
            {
                case MyLoggerLevel.Info:
                case MyLoggerLevel.Debug:
                    Debug.Log(message);
                    break;
                case MyLoggerLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                case MyLoggerLevel.Error:
                    Debug.LogError(message);
                    break;
                case MyLoggerLevel.Fatal:
                    // We are not using exceptions because it is slow on some platforms
                    // so we're going to fake in a little bit.
                    Debug.LogError("Exception: " + message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}

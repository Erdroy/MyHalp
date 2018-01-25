// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

#define USE_MYLOGGER

namespace MyHalp
{
    /// <summary>
    /// MyAssert class - implements loads of `.Assert` functions which can be used during runtime to check a wide variety of data types.
    /// </summary>
    public static class MyAssert
    {
        /// <summary>
        /// Check `data` for null, if it's null it logs 'failMessage' as Error.
        /// It returns true when it failed. Use like: if(MyAssert.Assert(gameObject, "Failed!"))
        /// </summary>
        public static bool Assert(UnityEngine.Object data, string failMessage, MyLoggerLevel logLevel = MyLoggerLevel.Warning)
        {
            if (data == null)
            {
#if USE_MYLOGGER
                MyLogger.Add(failMessage, logLevel);
#else
                UnityLog.Log(failMessage, logLevel);
#endif
                return true;
            }

            return false;
        }

        // TODO: more implementations
    }
}

// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

namespace MyHalp
{
    /// <summary>
    /// MyAssert class - implements loads of `.Assert` functions which can be used during runtime to check a wide variety of data types.
    /// </summary>
    public static class MyAssert
    {
        /// <summary>
        /// Check `data` for null, if it's null it logs 'failMessage' as Error.
        /// </summary>
        public static void Assert(UnityEngine.Object data, string failMessage, MyLoggerLevel logLevel = MyLoggerLevel.Warning)
        {
            if (data == null)
            {
                MyLogger.Add(failMessage, logLevel);
            }
        }

        // TODO: more implementations
    }
}

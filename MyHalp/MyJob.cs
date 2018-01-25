// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using System;
using System.Threading;

namespace MyHalp
{
    /// <summary>
    /// MyJob class, allows to execute threaded jobs.
    /// </summary>
    public sealed class MyJob
    {
        /// <summary>
        /// Run new job.
        /// </summary>
        /// <param name="callback">Delegate or something. x => SomeMethod() or delegate{ some; code; }</param>
        public static void Run(WaitCallback callback)
        {
            ThreadPool.QueueUserWorkItem(callback);
        }

        /// <summary>
        /// Waits for state = false. 
        /// This will lock the current thread until state will be false.
        /// </summary>
        /// <param name="state">The state, set to false, when wan't to break.</param>
        /// <param name="precision">
        /// The precision in miliseconds, 
        /// how much time this can be late 
        /// - this is need for optimization of the method.
        /// </param>
        public static void Wait(ref bool state, int precision = 5)
        {
            while (state) // loop while the state is true
            {
                // sleep
                Thread.Sleep(precision);
            }
        }

        /// <summary>
        /// Sleep for given amout of miliseconds.
        /// </summary>
        /// <param name="miliseconds">The amount of miliseconds to sleep.</param>
        public static void Wait(int miliseconds)
        {
            Thread.Sleep(miliseconds);
        }

        /// <summary>
        /// Sleep for given amount of seconds.
        /// </summary>
        /// <param name="seconds">The amount of seconds to sleep.</param>
        public static void Wait(float seconds)
        {
            var miliseconds = (int)(seconds * 1000.0f);
            Thread.Sleep(miliseconds);
        }

        [Obsolete("Obslete, use Wait(float) instead.")]
        public static void WaitSeconds() { }
    }
}
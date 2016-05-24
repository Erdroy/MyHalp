// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System.Threading;

namespace MyHalp
{
    public class MyJob
    {
        /// <summary>
        /// Run new job.
        /// </summary>
        /// <param name="callback">Delegate or something. x => SomeMethod() or delegate{ some; code; }</param>
        public static void Run(WaitCallback callback)
        {
            // TODO: Create own thread instead of using this.
            ThreadPool.QueueUserWorkItem(callback);
        }
    }
}
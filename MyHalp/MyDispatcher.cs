// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyDispatcher class, allows to dispatch calls from any thread on to the main thread.
    /// </summary>
    public sealed class MyDispatcher
    {
        private static readonly List<WaitCallback> DispatchQueue = new List<WaitCallback>();
        
        // ReSharper disable once ClassNeverInstantiated.Local
        private sealed class MyDispatcherHandle : MyComponent
        {
            protected override void OnUpdate()
            {
                lock (DispatchQueue)
                {
                    if (DispatchQueue.Count <= 0)
                        return;

                    // Dispatch all queued callbacks.
                    foreach (var dispatchItem in DispatchQueue)
                    {
                        dispatchItem.Invoke(null);
                    }

                    // Clear dispatched callbacks.
                    DispatchQueue.Clear();
                }
            }
        }
        
        /// <summary>
        /// Returns true when a dispatcher was initialized.
        /// </summary>
        public static bool IsInitialized => Handle != null;

        /// <summary>
        /// Initialize the dispatcher.
        /// </summary>
        public static void Init()
        {
            if (Handle == null)
                Handle = MyInstancer.Create<MyDispatcherHandle>();
        }
        
        /// <summary>
        /// Dispatch a message.
        /// Message/Callback will be run in the main-thread.
        /// </summary>
        /// <param name="callback">The callback</param>
        public static void Dispatch(WaitCallback callback)
        {
            lock (DispatchQueue)
            {
                DispatchQueue.Add(callback);
            }
        }

        /// <summary>
        /// The dispatcher handle, used for asset loading etc.
        /// </summary>
        public static MonoBehaviour Handle { get; private set; }
    }
}
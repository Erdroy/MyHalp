// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.Threading;

namespace MyHalp
{
    /// <summary>
    /// MyDispatcher class, allows to dispatch calls from any thead on to the main thead.
    /// </summary>
    public sealed class MyDispatcher
    {
        private static readonly List<WaitCallback> DispatchQueue = new List<WaitCallback>();
        private static MyDispatcherHandle _handle;
        
        // ReSharper disable once ClassNeverInstantiated.Local
        private sealed class MyDispatcherHandle : MyComponent
        {
            protected override void OnTick()
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
        public static bool IsInitialized
        {
            get
            {
                return _handle != null;
            }
        }
        
        [Obsolete("This is not needed anymore, will be removed in future release.")]
        public static void Init() { }
        
        /// <summary>
        /// Dispatch a message.
        /// Message/Callback will be run in the main-thread.
        /// </summary>
        /// <param name="callback">The callback</param>
        public static void Dispatch(WaitCallback callback)
        {
            if (_handle == null)
                _handle = MyInstancer.Create<MyDispatcherHandle>();

            lock (DispatchQueue)
            {
                DispatchQueue.Add(callback);
            }
        }
    }
}
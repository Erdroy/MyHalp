// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MyHalp
{
    public class MyDispatcher
    {
        private static readonly List<WaitCallback> DispatchQueue = new List<WaitCallback>();
        private static MyDispatcherHandle _handle;
        
        // ReSharper disable once ClassNeverInstantiated.Local
        private class MyDispatcherHandle : MyComponent
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

        /// <summary>
        /// Initialize the dispatcher.
        /// </summary>
        public static void Init()
        {
            if(_handle)
                throw new UnityException("Cannot initialize new dispatcher! There is actually initialised one.");

            _handle = MyInstancer.Create<MyDispatcherHandle>();
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
    }
}
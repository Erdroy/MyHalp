// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MyHalp
{
    public class MyDispatcher
    {
        private static volatile List<WaitCallback> _dispatchQueue = new List<WaitCallback>();
        private static GameObject _handle;
        
        private class MyDispatcherHandle : MyComponent
        {
            protected override void OnTick()
            {
                if (_dispatchQueue.Count <= 0)
                    return;

                foreach (var dispatchItem in _dispatchQueue)
                {
                    dispatchItem.Invoke(null);
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

            _handle = new GameObject("MyDispatcher - handle");
            _handle.AddComponent<MyDispatcherHandle>();
            Object.DontDestroyOnLoad(_handle);
        }

        /// <summary>
        /// Dispose the dispatcher.
        /// </summary>
        public static void Dispose()
        {
            if (!_handle)
                throw new UnityException("Cannot dispose dispatcher, because there is no any initialised dispatcher.");

            Object.DestroyImmediate(_handle);
        }

        /// <summary>
        /// Dispatch a message.
        /// Message/Callback will be run in the main-thread.
        /// </summary>
        /// <param name="callback">The callback</param>
        public static void Dispatch(WaitCallback callback)
        {
            _dispatchQueue.Add(callback);
        }
    }
}


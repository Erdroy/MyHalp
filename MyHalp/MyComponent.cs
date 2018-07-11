// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski
// ReSharper disable UnusedMember.Local

using System;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyComponent class - custom Behavior class,
    /// derives from MonoBehavior.
    /// </summary>
    public class MyComponent : MonoBehaviour
    {
        [HideInInspector] public Transform MyTransform;
        
        #region Overrides
        [Obsolete("Deprecated due to performance issues, please use standard Unity's Start function")]
        protected virtual void OnStart() { }

        [Obsolete("Deprecated due to performance issues, please use standard Unity's Update function")]
        protected virtual void OnUpdate() { }

        [Obsolete("Deprecated due to performance issues, please use standard Unity's LateUpdate function")]
        protected virtual void OnLateUpdate() { }

        [Obsolete("Deprecated due to performance issues, please use standard Unity's FixedUpdate function")]
        protected virtual void OnFixedUpdate() { }
        #endregion

        /// <summary>
        /// Disable the component.
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Enable the component.
        /// </summary>
        public void Enable()
        {
            enabled = true;
        }

        /// <summary>
        /// Returns true when component is enabled.
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled()
        {
            return enabled;
        }

        /// <summary>
        /// Creates instance when needed.
        /// Can be used for managers and anything else which needs to be easily accessed.
        /// </summary>
        /// <typeparam name="T">The derived type of object.</typeparam>
        [Obsolete("Please use MySingleton instead.")]
        public class Singleton<T> : MyComponent where T : MyComponent
        {
            private static T _instance;

            /// <summary>
            /// Destroys the instance.
            /// </summary>
            public static void Destroy()
            {
                Destroy(_instance);
                _instance = null;
            }

            public static T Instance => _instance ?? (_instance = MyInstancer.Create<T>());
        }
    }
}
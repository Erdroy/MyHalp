// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski
// ReSharper disable UnusedMember.Local

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

        #region Unity's functions

        private void Start()
        {
            MyTransform = transform;
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void LateUpdate()
        {
            OnLateUpdate();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }
        
        #endregion

        #region Overrides
        protected virtual void OnStart() { }

        protected virtual void OnUpdate() { }

        protected virtual void OnLateUpdate() { }

        protected virtual void OnFixedUpdate() { }

        protected virtual void OnDestroy() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }
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
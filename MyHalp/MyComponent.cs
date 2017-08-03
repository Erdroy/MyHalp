// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski
// ReSharper disable UnusedMember.Local

using System;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyComponent class - custom Behaviour class,
    /// derives from MonoBehaviour.
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

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }
        
        #endregion

        #region Overrides

        [Obsolete("Use OnStart instead")]
        protected virtual void OnInit()
        {
        }

        [Obsolete("Use OnUpdate instead")]
        protected virtual void OnTick()
        {
        }

        [Obsolete("Use OnFixedUpdate instead")]
        protected virtual void OnPhysicsTick()
        {
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnFixedUpdate()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }
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
    }
}
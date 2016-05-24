// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.
// ReSharper disable UnusedMember.Local

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
            OnInit();
        }

        private void Update()
        {
            OnTick();
        }

        private void FixedUpdate()
        {
            OnPhysicsTick();
        }

        // ReSharper disable once InconsistentNaming
        private void OnGUI()
        {
            OnDrawInterface();
        }

        #endregion

        #region Overrides

        protected virtual void OnInit()
        {
        }

        protected virtual void OnTick()
        {
        }

        protected virtual void OnPhysicsTick()
        {
        }

        protected virtual void OnDrawInterface()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        #endregion

        /// <summary>
        /// Disable the component.
        /// </summary>
        public void Disable()
        {
            if (IsEnabled())
                enabled = false;
        }

        /// <summary>
        /// Enable the component.
        /// </summary>
        public void Enable()
        {
            if (!IsEnabled())
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
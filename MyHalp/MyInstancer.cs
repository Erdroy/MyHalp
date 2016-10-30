// MyHalp © 2016 Damian 'Erdroy' Korczowski, Mateusz 'Maturas' Zawistowski and contibutors.

using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyInstancer - Helps with instancing handles, etc.
    /// Mainly created for internal use in MyHalp.
    /// </summary>
    public class MyInstancer
    {
        private static GameObject _handle;

        /// <summary>
        /// Creates new instance of component.
        /// </summary>
        /// <typeparam name="T">The component.</typeparam>
        /// <returns>The created component.</returns>
        public static T Create<T>() where T : Component
        {
            // check if the instancer has been initialized,
            // if not, initialize.
            if (_handle == null)
            {
                _handle = new GameObject("MyInstancer");
            }
            
            return _handle.AddComponent<T>();
        }
    }
}
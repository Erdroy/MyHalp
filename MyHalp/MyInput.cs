// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System.Collections.Generic;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// The MyInputState enumerator.
    /// </summary>
    public enum MyInputState : byte
    {
        Down,
        Up,
        Held,
        None
    }
    
    /// <summary>
    /// Custom Input class.
    /// Supports FixedUpdate with FixedGet().
    /// </summary>
    /// <typeparam name="TMyInputKeys">Enumerator that holds all types of keys which you need.</typeparam>
    public class MyInput<TMyInputKeys>
    {
        /// <summary>
        /// Contains all binded keys, for adding new binds use MyInput.Bind(...) method.
        /// </summary>
        public static readonly Dictionary<TMyInputKeys, List<KeyCode>> BindedKeys = new Dictionary<TMyInputKeys, List<KeyCode>>();

        // private
        private static readonly List<TMyInputKeys> StateList = new List<TMyInputKeys>();
        private static readonly List<TMyInputKeys> LastStateList = new List<TMyInputKeys>();
        private static readonly List<TMyInputKeys> FixedStateList = new List<TMyInputKeys>();
        private static readonly List<TMyInputKeys> FixedLastStateList = new List<TMyInputKeys>();
        
        /// <summary>
        /// Gets a input state from a key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Input state.</returns>
        public static MyInputState Get(TMyInputKeys key)
        {
            var flag1 = StateList.Contains(key);
            var flag2 = LastStateList.Contains(key);
            
            if (flag1 && flag2)
                return MyInputState.Held;

            if (!flag1 && flag2)
                return MyInputState.Up;

            return flag1 ? MyInputState.Down : MyInputState.None;
        }

        /// <summary>
        /// Gets a input state from a key. Use in FixedUpdate()!
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Input state.</returns>
        public static MyInputState FixedGet(TMyInputKeys key)
        {
            var flag1 = FixedStateList.Contains(key);
            var flag2 = FixedLastStateList.Contains(key);

            if (flag1 && flag2)
                return MyInputState.Held;

            if (!flag1 && flag2)
                return MyInputState.Up;

            return flag1 ? MyInputState.Down : MyInputState.None;
        }

        /// <summary>
        /// Binds a key to a keyCode.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="keyCode">The keycode.</param>
        public static void Bind(TMyInputKeys key, KeyCode keyCode)
        {
            if (BindedKeys.ContainsKey(key))
            {
                var keys = BindedKeys[key];
                if (!keys.Contains(keyCode))
                {
                    keys.Add(keyCode);
                }
            }
            else
            {
                BindedKeys.Add(key, new List<KeyCode> { keyCode });
            }
        }

        /// <summary>
        /// Clears all binds.
        /// </summary>
        public static void ClearBinds()
        {
            BindedKeys.Clear();
        }

        /// <summary>
        /// Update input frame
        /// </summary>
        public static void Update()
        {
            LastStateList.Clear();
            LastStateList.AddRange(StateList);
            StateList.Clear();

            foreach (var key in BindedKeys)
            {
                foreach (var keyCode in key.Value)
                {
                    if (Input.GetKey(keyCode))
                    {
                        StateList.Add(key.Key);
                    }
                }
            }
        }

        /// <summary>
        /// Update input fixed-timestep frame
        /// </summary>
        public static void FixedUpdate()
        {
            FixedLastStateList.Clear();
            FixedLastStateList.AddRange(FixedStateList);
            FixedStateList.Clear();

            foreach (var key in BindedKeys)
            {
                foreach (var keyCode in key.Value)
                {
                    if (Input.GetKey(keyCode))
                    {
                        FixedStateList.Add(key.Key);
                    }
                }
            }
        }
    }
}
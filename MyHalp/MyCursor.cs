// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;

namespace MyHalp
{
    /// <summary>
    /// MyCursorState enum.
    /// </summary>
    public enum MyCursorState
    {
        Show,
        Hide
    }

    /// <summary>
    /// MyCursor class - cursor management helper based on stack.
    /// For default cursor is always shown.
    /// </summary>
    public static class MyCursor
    {
        private static readonly Stack<MyCursorState> Stack = new Stack<MyCursorState>();
        private static MyCursorState _defaultState;
        
        /// <summary>
        /// Sets the default state for cursor.
        /// </summary>
        /// <param name="defaultState">The state.</param>
        public static void SetDefault(MyCursorState defaultState = MyCursorState.Show)
        {
            _defaultState = defaultState;
        }

        /// <summary>
        /// Pushes new cursor state and sets it as current.
        /// </summary>
        /// <param name="state">The cursor state.</param>
        public static void Push(MyCursorState state)
        {
            Stack.Push(state);
            Use(state);
        }

        /// <summary>
        /// Pops the current cursor state and sets the stack peek or default as current.
        /// </summary>
        public static void Pop()
        {
            if(Stack.Count > 0)
                Stack.Pop(); 

            Use(Stack.Count > 0 ? Stack.Peek() : _defaultState);
        }

        /// <summary>
        /// Clears cursor state stack, default state is being used.
        /// </summary>
        public static void Clear()
        {
            Stack.Clear();
        }

        // private
        private static void Use(MyCursorState state)
        {
            switch (state)
            {
                case MyCursorState.Show:
                    UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;
                    UnityEngine.Cursor.visible = true;
                    break;
                case MyCursorState.Hide:
                    UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
                    UnityEngine.Cursor.visible = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}

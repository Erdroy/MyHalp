// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyTimer precision settings.
    /// </summary>
    public enum MyTimerPrecision
    {
        /// <summary>
        /// Low precision, using Unity's coroutine, low resources consumption.
        /// </summary>
        Low,

        // TODO: Medium, some more precision but dispatched and less resouces-eating?

        /// <summary>
        /// High precision, using MyJob, method isn't called from non-main thread, high resources consumption.
        /// </summary>
        High
    }

    /// <summary>
    /// MyTimer class, allows to run methods with delay.
    /// </summary>
    public class MyTimer : MyComponent
    {
   
        private static MyTimer _instance;
        private static uint _id;

        private static readonly List<uint> Actions = new List<uint>();
        
        /// <summary>
        /// Initialize MyTimer component.
        /// </summary>
        public static void Init()
        {
            _instance = MyInstancer.Create<MyTimer>();
        }

        /// <summary>
        /// Runs delayed method.
        /// </summary>
        /// <param name="time">How much time MyTimmer must wait before calling the method.</param>
        /// <param name="method">The method to be called after a delay.</param>
        /// <param name="precision">Delay time precision.</param>
        /// <returns>The id of current timer action, can be used to cancel delay.</returns>
        public static uint Delay(float time, Action method, MyTimerPrecision precision = MyTimerPrecision.Low)
        {
            if (method == null)
            {
                Debug.LogError("Cannot delay method with no method!");
                return 0;
            }

            _id++;

            lock (Actions)
                Actions.Add(_id);

            switch (precision)
            {
                case MyTimerPrecision.Low:
                    _instance.StartCoroutine(SwitchT1(method, time, _id));
                    break;
                case MyTimerPrecision.High:
                    MyJob.Run(delegate
                    {
                        MyJob.Wait(time);
                        method();
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(precision), precision, null);
            }

            return _id;
        }

        /// <summary>
        /// Cancel delayed method call by id.
        /// </summary>
        /// <param name="id">The delayed method call.</param>
        public static void Cancel(uint id)
        {
            // cancel

            lock (Actions)
            {
                if (Actions.Contains(id))
                    Actions.Remove(id);
            }
        }

        // private
        private static IEnumerator SwitchT1(Action onDone, float time, uint id)
        {
            yield return new WaitForSeconds(time);

            lock (Actions)
            {
                if (!Actions.Contains(id)) // if the action was canceled
                    yield break;

                onDone?.Invoke();
                Actions.Remove(id);
            }
        }
    }
}

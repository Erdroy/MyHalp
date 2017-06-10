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
                    _instance.StartCoroutine(Delay(method, time, _id));
                    break;
                case MyTimerPrecision.High:
                    MyJob.Run(delegate
                    {
                        MyJob.Wait(time);

                        lock (Actions)
                        {
                            if (!Actions.Contains(_id))
                                return;

                            try
                            {
                                method();
                            }
                            catch (Exception ex)
                            {
                                MyLogger.Add("MyTimer: " + ex, MyLoggerLevel.Error);
                            }
                            Actions.Remove(_id);
                        }
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(precision), precision, null);
            }

            return _id;
        }

        /// <summary>
        /// Runs method in interval.
        /// </summary>
        /// <param name="interval">How much time MyTimmer must wait before calling the method.</param>
        /// <param name="method">The method to be called after a delay.</param>
        /// <param name="precision">Interval time precision.</param>
        /// <returns>The id of current timer action, can be used to cancel delay.</returns>
        public static uint Interval(float interval, Action method, MyTimerPrecision precision = MyTimerPrecision.Low)
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
                    _instance.StartCoroutine(Interval(method, interval, _id));
                    break;
                case MyTimerPrecision.High:
                    MyJob.Run(delegate
                    {
                        while (true)
                        {
                            MyJob.Wait(interval);

                            lock (Actions)
                                if (!Actions.Contains(_id))
                                    return;

                            method();

                            lock (Actions)
                                Actions.Remove(_id);
                        }
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(precision), precision, null);
            }

            return _id;
        }
        
        /// <summary>
        /// Cancel delayed/interval method call by id.
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
        private static IEnumerator Delay(Action onDone, float time, uint id)
        {
            yield return new WaitForSeconds(time);

            lock (Actions)
                if (!Actions.Contains(id)) // if the action was canceled
                    yield break;
            try
            {
                onDone?.Invoke();
            }
            catch (Exception ex)
            {
                MyLogger.Add("MyTimer: " + ex, MyLoggerLevel.Error);
            }

            lock (Actions)
                Actions.Remove(id);
        }

        // private
        private static IEnumerator Interval(Action onDone, float time, uint id)
        {
            while (true)
            {
                yield return new WaitForSeconds(time);

                lock (Actions)
                    if (!Actions.Contains(id)) // if the action was canceled
                        yield break;

                try
                {
                    onDone?.Invoke();
                }
                catch (Exception ex)
                {
                    MyLogger.Add("MyTimer: " + ex, MyLoggerLevel.Error);
                }

                lock (Actions)
                    Actions.Remove(id);
            }
        }
    }
}

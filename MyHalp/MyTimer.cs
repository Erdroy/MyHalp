// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

#define USE_MYLOGGER

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
        /// Low precision, using Unity's co-routine, low resources consumption.
        /// </summary>
        Low,

        // TODO: Medium, some more precision but dispatched and less resource-eating?

        /// <summary>
        /// High precision, using MyJob, method isn't called from non-main thread, high resources consumption.
        /// </summary>
        High
    }

    /// <summary>
    /// MyTimer class, allows to run methods with delay.
    /// </summary>
    public class MyTimer : MyComponent.Singleton<MyTimer>
    {
        private static uint _id;
        private static readonly List<uint> Actions = new List<uint>();

        /// <summary>
        /// Initialize MyTimer component.
        /// </summary>
        [Obsolete("Init call is no longer required")]
        public static void Init()
        {
        }

        /// <summary>
        /// Runs delayed method.
        /// </summary>
        /// <param name="time">How much time MyTimer must wait before calling the method.</param>
        /// <param name="method">The method to be called after a delay.</param>
        /// <param name="precision">Delay time precision.</param>
        /// <returns>The id of current timer action, can be used to cancel delay.</returns>
        public static uint Delay(float time, Action method, MyTimerPrecision precision = MyTimerPrecision.Low)
        {
            if (method == null)
            {
#if USE_MYLOGGER
                MyLogger.Add("Cannot delay method with no method!", MyLoggerLevel.Error);
#else
                UnityLog.Log("Cannot delay method with no method!", MyLoggerLevel.Error);
#endif
                return 0;
            }

            _id++;

            lock (Actions)
                Actions.Add(_id);

            switch (precision)
            {
                case MyTimerPrecision.Low:
                    Instance.StartCoroutine(Instance.Delay(method, time, _id));
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
#if USE_MYLOGGER
                                MyLogger.Add("MyTimer: " + ex, MyLoggerLevel.Error);
#else
                                UnityLog.Log("MyTimer: " + ex, MyLoggerLevel.Error);
#endif
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
#if USE_MYLOGGER
                MyLogger.Add("Cannot delay method with no method!", MyLoggerLevel.Error);
#else
                UnityLog.Log("Cannot delay method with no method!", MyLoggerLevel.Error);
#endif
                return 0;
            }

            _id++;

            lock (Actions)
                Actions.Add(_id);

            switch (precision)
            {
                case MyTimerPrecision.Low:
                    Instance.StartCoroutine(Instance.Interval(method, interval, _id));
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
        private IEnumerator Delay(Action onDone, float time, uint id)
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
#if USE_MYLOGGER
                MyLogger.Add("MyTimer: " + ex, MyLoggerLevel.Error);
#else
                UnityLog.Log("MyTimer: " + ex, MyLoggerLevel.Error);
#endif
            }

            lock (Actions)
                Actions.Remove(id);
        }

        // private
        private IEnumerator Interval(Action onDone, float time, uint id)
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
#if USE_MYLOGGER
                    MyLogger.Add("MyTimer: " + ex, MyLoggerLevel.Error);
#else
                    UnityLog.Log("MyTimer: " + ex, MyLoggerLevel.Error);
#endif
                }
            }
        }
    }
}

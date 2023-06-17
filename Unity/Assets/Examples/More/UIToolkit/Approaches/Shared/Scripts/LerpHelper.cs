using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Approaches
{
    /// <summary>
    /// Help with programmatic animations via linear interpolation ("Lerp")
    /// </summary>
    public static class LerpHelper
    {
        //  Methods ---------------------------------------
        
        
        /// <summary>
        /// Lerps a value over time, calling a callback each time.
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="durationMilliseconds"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <param name="callback"></param>
        public static async UniTask LerpValueAsync(
            float fromValue, 
            float toValue, 
            int durationMilliseconds, 
            CancellationTokenSource cancellationTokenSource, 
            Action<float> callback)
        {
            float elapsedMilliseconds = 0f;
            int deltaMilliseconds = (int)durationMilliseconds / 10;
            while (elapsedMilliseconds < durationMilliseconds)
            {
                try
                {
                    await UniTask.Delay(deltaMilliseconds,
                        DelayType.Realtime,
                        PlayerLoopTiming.Update,
                        cancellationTokenSource.Token);
                }
                catch (OperationCanceledException) {}

                elapsedMilliseconds += deltaMilliseconds;
                float p = elapsedMilliseconds / durationMilliseconds;
                float easeOut = 1 - ((1 - p) * (1 - p));
                
                float nextValue = Mathf.Lerp(fromValue, toValue, easeOut);
                callback.Invoke(nextValue);
            }
        }
    }
}
using System;
using System.Collections;
using GameManagers;
using UnityEngine;

namespace Utilities
{
    public static class TFWToolKit
    {
        public static void Timer(float p_duration, Action p_callback)
        {
            SceneManager.instance.StartCoroutine(TimerCoRoutine(p_duration, p_callback));
        }

        private static IEnumerator TimerCoRoutine(float p_duration, Action p_callback)
        {
            yield return new WaitForSeconds(p_duration);

            p_callback?.Invoke();
        }
    }
}

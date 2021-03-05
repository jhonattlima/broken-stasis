using System;
using System.Collections;
using System.Collections.Generic;
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

        public static List<Sprite> GetSpriteShuffledSubList(List<Sprite> p_inputList, int p_sublistSize)
        {
            List<Sprite> __shuffledSubList = new List<Sprite>();

            if(p_sublistSize < p_inputList.Count)
            {
                for(int i = 0; i < p_sublistSize; i++)
                {
                    int __randomIndex = UnityEngine.Random.Range(0, p_inputList.Count - 1);
                    
                    __shuffledSubList.Add(p_inputList[__randomIndex]);

                    p_inputList.RemoveAt(__randomIndex);
                }
            }

            return __shuffledSubList;
        }
    }
}

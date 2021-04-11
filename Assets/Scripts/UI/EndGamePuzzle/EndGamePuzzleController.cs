using System;
using System.Collections;
using GameManagers;
using UnityEngine;

namespace UI.EndGamePuzzle
{
    public class EndGamePuzzleController
    {
        public Action<int> onBarCompleted;
        public Action onAllBarsCompleted;
        
        // TODO: Extract this consts to variables
        private const float LOADING_TIME_IN_SECONDS = 10f;
        private const float LOAD_PERCENTAGE_SPLIT = 100f;
        private const int TOTAL_BARS_COUNT = 3;
        
        private PuzzleLoadingProgress _loadingProgress;
        public PuzzleLoadingProgress loadingProgress 
        {
            get { return _loadingProgress; }
        }

        private void Awake()
        {
            _loadingProgress = new PuzzleLoadingProgress();
        }

        public void StartLoading()
        {
            SceneManager.instance.StartCoroutine(LoadingCoRoutine());
        }

        public void StopLoading()
        {
            SceneManager.instance.StopCoroutine(LoadingCoRoutine());

            GameHudManager.instance.endGameUI.ResetBar(_loadingProgress.currentBar - 1);
        }

        private IEnumerator LoadingCoRoutine()
        {
            float __secondsPerIteration = LOADING_TIME_IN_SECONDS / LOAD_PERCENTAGE_SPLIT;
            float __percentagePerIteration = 1 / LOAD_PERCENTAGE_SPLIT;
            
            _loadingProgress.currentBarProgress = 0f;

            while(_loadingProgress.currentBarProgress < LOAD_PERCENTAGE_SPLIT)
            {
                _loadingProgress.currentBarProgress += __percentagePerIteration;

                GameHudManager.instance.endGameUI.UpdateBar(_loadingProgress.currentBar - 1, _loadingProgress.currentBarProgress);

                yield return new WaitForSeconds(__secondsPerIteration);
            }

            _loadingProgress.currentBar++;
            LoadingBarCompleted();

            yield return null;
        }

        private void LoadingBarCompleted()
        {
            onBarCompleted?.Invoke(_loadingProgress.currentBar);

            if(_loadingProgress.currentBar < TOTAL_BARS_COUNT)
                SceneManager.instance.StartCoroutine(LoadingCoRoutine());
            else
                onAllBarsCompleted?.Invoke();
        }
    }
}
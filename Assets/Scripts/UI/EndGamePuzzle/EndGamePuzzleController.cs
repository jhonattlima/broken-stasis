using System;
using System.Collections;
using GameManagers;
using UnityEngine;
using Utilities;

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

        private IEnumerator _loadCoRoutine;

        public EndGamePuzzleController()
        {
            _loadingProgress = new PuzzleLoadingProgress()
            {
                currentBar = 1,
                currentBarProgress = 0f
            };

            _loadCoRoutine = LoadingCoRoutine();
        }

        public void StartLoading()
        {
            InputController.GamePlay.MouseEnabled = false;

            CustomSceneManager.instance.StartCoroutine(_loadCoRoutine);
            
            GameHudManager.instance.endGameUI.ShowUI();
        }

        public void StopLoading()
        {
            InputController.GamePlay.MouseEnabled = true;

            StopLoadingBar();

            GameHudManager.instance.endGameUI.ResetBar(_loadingProgress.currentBar - 1);
        }

        private IEnumerator LoadingCoRoutine()
        {
            float __secondsPerIteration = LOADING_TIME_IN_SECONDS / LOAD_PERCENTAGE_SPLIT;
            float __percentagePerIteration = 1 / LOAD_PERCENTAGE_SPLIT;
            
            _loadingProgress.currentBarProgress = 0f;

            while(_loadingProgress.currentBarProgress < 1f)
            {
                _loadingProgress.currentBarProgress += __percentagePerIteration;

                GameHudManager.instance.endGameUI.UpdateBar(_loadingProgress.currentBar - 1, _loadingProgress.currentBarProgress);

                yield return new WaitForSeconds(__secondsPerIteration);
            }

            LoadingBarCompleted();

            yield return null;
        }

        private void LoadingBarCompleted()
        {
            StopLoadingBar();
           
            onBarCompleted?.Invoke(_loadingProgress.currentBar);

            _loadingProgress.currentBar++;

            if (_loadingProgress.currentBar <= TOTAL_BARS_COUNT)
                CustomSceneManager.instance.StartCoroutine(_loadCoRoutine);
            else
            {
                InputController.GamePlay.MouseEnabled = true;
                onAllBarsCompleted?.Invoke();
            }

        }

        private void StopLoadingBar()
        {
            CustomSceneManager.instance.StopCoroutine(_loadCoRoutine);

            _loadCoRoutine = LoadingCoRoutine();
        }
    }
}
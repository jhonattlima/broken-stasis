using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Minigame
{
    public class MinigameUI : MonoBehaviour
    {
        [SerializeField] private Animator _hudAnimator;
        [SerializeField] private Image[] _codeImages;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private TextMeshProUGUI _countdownText;
        [SerializeField] private Sprite _collectedImage;
        [SerializeField] private MinigameUIAnimationEventHandler _minigameEventHandler;

        private MinigameLogic _minigameLogic;
        private MinigameStateEnum _minigameEndState = MinigameStateEnum.NONE;

        public Action onMinigameSuccess;
        public Action onMinigameFailed;

        private const string SHOW_MINIGAME_HUD_ANIMATION = "Show";
        private const string HIDE_MINIGAME_HUD_ANIMATION = "Hide";

        private void Awake()
        {
            _minigameEventHandler.OnHideAnimationEnd = HandleHideAnimationEnd;
            _minigameEventHandler.OnShowAnimationEnd = HandleShowAnimationEnd;

            _minigameLogic = new MinigameLogic(
                _codeImages,
                _buttons,
                _countdownText,
                _collectedImage
            );

            _minigameLogic.onMinigameFinished = HandleMinigameEnded;
        }

        private void HandleMinigameEnded(MinigameStateEnum p_finishedState)
        {
            _hudAnimator.Play(HIDE_MINIGAME_HUD_ANIMATION);
            _minigameEndState = p_finishedState;
        }

        private void HandleHideAnimationEnd()
        {
            InputController.GamePlay.InputEnabled = true;

            switch(_minigameEndState)
            {
                case MinigameStateEnum.SUCCESSFULL:
                    onMinigameSuccess?.Invoke();
                    break;
                case MinigameStateEnum.FAILED:
                    onMinigameFailed?.Invoke();
                    break;
                default:
                    break;
            }          
        }

        private void HandleShowAnimationEnd()
        {
            _minigameLogic.StartCountDown();
        }

        public void ShowMinigame()
        {
            InputController.GamePlay.InputEnabled = false;

            _minigameLogic.InitializeMinigame();

            _hudAnimator.Play(SHOW_MINIGAME_HUD_ANIMATION);
        }
    }
}
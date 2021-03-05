using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Minigame
{
    public class MinigameUI
    {
        [SerializeField] private Animator _hudAnimator;
        [SerializeField] private Image[] _codeImages;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private TextMeshProUGUI _countdownText;
        [SerializeField] private MinigameUIAnimationEventHandler _minigameEventHandler;

        private MinigameLogic _minigameLogic;

        public Action onMinigameSuccess;
        public Action onMinigameFailed;

        private const string SHOW_MINIGAME_HUD_ANIMATION = "Show";
        private const string HIDE_MINIGAME_HUD_ANIMATION = "Hide";

        private void Awake()
        {
            _minigameEventHandler.OnHideAnimationEnd = HandleHideAnimationEnd;
            _minigameEventHandler.OnShowAnimationEnd = HandleShowAnimationEnd;
        }

        private void HandleHideAnimationEnd()
        {
            switch(_minigameLogic.state)
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
            _minigameLogic.InitializeMinigame();

            _hudAnimator.Play(SHOW_MINIGAME_HUD_ANIMATION);
        }
    }
}
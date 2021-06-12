using JetBrains.Annotations;
using SaveSystem;
using UnityEngine;
using Utilities.VariableManagement;

namespace UI.MainMenu
{
    public class MenuSceneController : MonoBehaviour
    {
        [SerializeField] private Animator _titleAnimator;
        [SerializeField] private TitleMenuAnimationEventHandler _titleAnimationHandler;
        [SerializeField] private MenuScreenAnimationController _mainMenuAnimationController;
        [SerializeField] private MenuScreenAnimationController _slotScreenAnimationController;
        [SerializeField] private MenuScreenAnimationController _optionsScreenAnimationController;

        private MenuState _currentState;
        private MenuScreenAnimationController _currentAnimationController;

        private bool _isDetectingInput;

        private void Start()
        {
            _currentState = MenuState.TITLE;
            _currentAnimationController = null;
            _isDetectingInput = false;

            LoadingView.instance.FadeOut(null, VariablesManager.uiVariables.defaultFadeInSpeed);

            _titleAnimationHandler.OnFadeEnd = HandleInputEnabled;
            _titleAnimationHandler.OnPressButton = HandleAnyButtonPressed;

            SaveGameManager.instance.Initialize();
        }

        private void Update()
        {
            if(_isDetectingInput && Input.anyKeyDown)
            {
                _titleAnimator.Play("PressedButton");
                _isDetectingInput = false;
            }
        }

        private void HandleInputEnabled()
        {
            _isDetectingInput = true;
        }

        private void HandleAnyButtonPressed()
        {
            ChangeToScreen(MenuState.MAIN_MENU);
        }

        [UsedImplicitly]
        public void OpenSlots()
        {
            ChangeToScreen(MenuState.SLOT_SCREEN);
        }

        [UsedImplicitly]
        public void BackToMenu()
        {
            ChangeToScreen(MenuState.MAIN_MENU);
        }

        [UsedImplicitly]
        public void OpenOptions()
        {
            ChangeToScreen(MenuState.OPTIONS_SCREEN);
        }

        private void ChangeToScreen(MenuState p_nextScreen)
        {
            _currentState = p_nextScreen;

            if(_currentState == MenuState.MAIN_MENU)
            {
                if(_currentAnimationController == null)
                {
                    _mainMenuAnimationController.Show(delegate () 
                    {
                        _currentAnimationController = _mainMenuAnimationController;
                    });
                }
                else
                {
                    _currentAnimationController.Hide(delegate () 
                    {
                        _mainMenuAnimationController.Show(delegate () 
                        {
                            _currentAnimationController = _mainMenuAnimationController;
                        });
                    });
                }
            }
            else if(_currentState == MenuState.SLOT_SCREEN)
            {
                _currentAnimationController.Hide(delegate () 
                {
                    _slotScreenAnimationController.Show(delegate () 
                    {
                        _currentAnimationController = _slotScreenAnimationController;
                    });
                });
            }
            else if(_currentState == MenuState.OPTIONS_SCREEN)
            {
                _currentAnimationController.Hide(delegate () 
                {
                    _optionsScreenAnimationController.Show(delegate () 
                    {
                        _currentAnimationController = _optionsScreenAnimationController;
                    });
                });
            }
        }
    }
}

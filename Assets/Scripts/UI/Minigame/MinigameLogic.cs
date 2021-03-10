﻿using System;
using System.Collections.Generic;
using GameManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Audio;
using Utilities.VariableManagement;

namespace UI.Minigame
{
    public class MinigameLogic
    {
        private Image[] _codeImages;
        private Button[] _buttons;
        private TextMeshProUGUI _countdownText;
        private Sprite _completedImage;

        private Coroutine _countdownTimer;

        private const string CODE_SPRITES_PATH = "";
        private List<Sprite> _codeSprites;

        public Action<MinigameStateEnum> onMinigameFinished;

        public MinigameLogic(
            Image[] p_codeImages,
            Button[] p_buttons,
            TextMeshProUGUI p_countdownText,
            Sprite p_completedImage
        )
        {
            _codeImages = p_codeImages;
            _buttons = p_buttons;
            _countdownText = p_countdownText;
            _completedImage = p_completedImage;
        }

        public void InitializeMinigame()
        {
            _codeSprites = new List<Sprite>(Resources.LoadAll<Sprite>("GeneratorCodeImages"));
            
            List<Sprite> __buttonImages = TFWToolKit.GetSpriteShuffledSubList(_codeSprites, _buttons.Length);

            for (int i = 0; i < _buttons.Length; i++)
                _buttons[i].image.sprite = __buttonImages[i];
            
            List<Sprite> __codeImages = TFWToolKit.GetSpriteShuffledSubList(__buttonImages, _codeImages.Length);

            for (int i = 0; i < _codeImages.Length; i++)
                _codeImages[i].sprite = __codeImages[i];

            InitializeButtons();

            _buttons[0].Select();

            _countdownTimer = null;
        }

        private void InitializeButtons()
        {
            foreach(Button __button in _buttons)
            {
                __button.onClick.RemoveAllListeners();
                __button.onClick.AddListener(delegate{HandleOnButtonClick(__button.image.sprite.name);});
            }
        }

        private void HandleOnButtonClick(string p_imageName)
        {
            for(int i=0; i<_codeImages.Length; i++)
            {
                if(_codeImages[i].sprite.name != _completedImage.name)
                {
                    Debug.Log("i " + i + " | ButtonImageName " + p_imageName + " | CodeImage " + _codeImages[i].sprite.name);
                    if(_codeImages[i].sprite.name == p_imageName)
                    {
                        _codeImages[i].sprite = _completedImage;
                        CheckSuccessfullFinish();
                    }
                    else
                        CallFailedFinish();

                    return;
                }
            }
        }

        private void CheckSuccessfullFinish()
        {
            if(_codeImages[_codeImages.Length - 1].sprite.name == _completedImage.name)
            {
                TFWToolKit.CancelTimer(_countdownTimer);
                
                AudioManager.instance.Play(AudioNameEnum.SUCCESS_MINIGAME);

                onMinigameFinished?.Invoke(MinigameStateEnum.SUCCESSFULL);
            }
        }

        private void CallFailedFinish()
        {
            AudioManager.instance.Play(AudioNameEnum.FAILED_MINIGAME);

            TFWToolKit.CancelTimer(_countdownTimer);

            onMinigameFinished?.Invoke(MinigameStateEnum.FAILED);
        }

        public void StartCountDown()
        {
            _countdownTimer = TFWToolKit.CountdownTimer(VariablesManager.uiVariables.generatorMinigameDuration, CallFailedFinish, delegate(TimeSpan p_timeSpan)
            {
                _countdownText.text = p_timeSpan.ToString(@"ss\.ff");
            });
        }
    }
}
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Minigame
{
    public class MinigameLogic
    {
        private Image[] _codeImages;
        private Button[] _buttons;
        private TextMeshProUGUI _countdownText;
        private Sprite _completedImage;

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

            // reseta countdown
        }

        private void InitializeButtons()
        {
            foreach(Button __button in _buttons)
            {
                __button.onClick.AddListener(delegate{HandleOnButtonClick(__button.image.sprite.name);});
            }
        }

        private void HandleOnButtonClick(string p_imageName)
        {
            for(int i=0; i<_codeImages.Length; i++)
            {
                // Se é uma img de código e não uma completa
                if(_codeImages[i].sprite.name != _completedImage.name)
                {
                    // deu match do código
                    if(_codeImages[i].sprite.name == p_imageName)
                    {
                        _codeImages[i].sprite = _completedImage;
                        CheckCompletion();
                    }
                    return;
                }
            }
        }

        private void CheckCompletion()
        {
            if(_codeImages[_codeImages.Length - 1].sprite.name == _completedImage.name)
                onMinigameFinished?.Invoke(MinigameStateEnum.SUCCESSFULL);
        }

        public void StartCountDown()
        {
            
        }
    }
}
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

        private const string CODE_SPRITES_PATH = "";
        private List<Sprite> _codeSprites;

        public MinigameStateEnum state { get; private set; }

        public MinigameLogic(
            Image[] p_codeImages,
            Button[] p_buttons,
            TextMeshProUGUI p_countdownText
        )
        {
            _codeImages = p_codeImages;
            _buttons = p_buttons;
            _countdownText = p_countdownText;
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

            // InitializeBotões (funções onClick)

            // reseta countdown
        }

        public void StartCountDown()
        {
            
        }
    }
}
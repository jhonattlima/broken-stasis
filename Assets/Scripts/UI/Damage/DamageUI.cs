using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Minigame
{
    public class DamageUI : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _playerLifeDamageSprites;
        [SerializeField] private Animator _animatorComponent;
        [SerializeField] private Image _currentImageComponent;

        private const string GET_HURT_HUD_ANIMATION = "GetHurt";

        private void Start()
        {
            _playerLifeDamageSprites.Reverse();
        }

        public void receiveHit(int _currentPlayerLife)
        {
            _currentImageComponent.sprite = _playerLifeDamageSprites[_currentPlayerLife - 1];
            _animatorComponent.Play(GET_HURT_HUD_ANIMATION);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum AreYouSureOptionsEnum
    {
        YES,
        NO
    }

    public class UIAreYouSure : MonoBehaviour
    {
        [SerializeField] private Button _buttonYes;
        [SerializeField] private Button _buttonNo;

        private Animator _animator;

        private const string ANIMATION_SHOW_PANEL = "Show";
        private const string ANIMATION_HIDE_PANEL = "Hide";

        private void Awake()
        {
            _animator = GetComponent<Animator>() ?? throw new MissingComponentException("Animator not found!");
        }

        public void StartOptionsHandlers(Action p_handleYesSelection, Action p_handleNoSelection = null)
        {
            _buttonYes.onClick.RemoveAllListeners();
            _buttonNo.onClick.RemoveAllListeners();

            _buttonYes.onClick.AddListener(delegate { p_handleYesSelection.Invoke(); });
            _buttonYes.onClick.AddListener(delegate { p_handleNoSelection?.Invoke(); });

            _animator.Play(ANIMATION_SHOW_PANEL);
        }

        public void HandleChosenOption(AreYouSureOptionsEnum _areYouSureOptionsEnum)
        {
            _animator.Play(ANIMATION_HIDE_PANEL);
        }
    }
}

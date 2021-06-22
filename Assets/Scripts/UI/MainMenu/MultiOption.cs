﻿using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace UI.MainMenu
{
    public class MultiOption : MonoBehaviour
    {
        [SerializeField] private string[] _options;
        [SerializeField] private TextMeshProUGUI _optionText;

        private string _currentOption;

        // TODO: Remove and initialize current option with initialize method
        private void Awake()
        {
            _currentOption = _optionText.text;
        }

        public void InitializeOptions(string p_currentOption, string[] p_options)
        {
            _currentOption = p_currentOption;
            _options = p_options;
        }

        [UsedImplicitly]
        public void NextOption()
        {
            for(int i = 0; i < _options.Length; i++)
            {
                if(_currentOption == _options[i])
                {
                    if(i < _options.Length - 1)
                        _currentOption = _options[i+1];
                    else
                        _currentOption = _options[0];
                    
                    break;
                }
            }
            
            _optionText.text = _currentOption;
        }

        [UsedImplicitly]
        public void PreviousOption()
        {
            for(int i = 0; i < _options.Length; i++)
            {
                if(_currentOption == _options[i])
                {
                    if(i > 0)
                        _currentOption = _options[i-1];
                    else
                        _currentOption = _options[_options.Length-1];
                    
                    break;
                }
            }
            
            _optionText.text = _currentOption;
        }

        public string GetCurrentOption()
        {
            return _currentOption;
        }
    }    
}

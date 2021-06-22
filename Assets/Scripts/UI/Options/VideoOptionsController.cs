using TMPro;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Options.Video
{
    public class VideoOptionsController : MonoBehaviour
    {
        [SerializeField] private MenuSceneController _menuSceneController;
        [SerializeField] private MultiOption _resolutionMultiOption;
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private Button _applyReturnButton;
        [SerializeField] private TextMeshProUGUI _applyReturnText;

        private bool _hasChanges;

        private bool _isFullscreen;
        private string _currentResolution;

        private void Start()
        {            
            LoadSettings();

            InitializeMultiOption();
            InitializeToggle();
        }

        private void LoadSettings()
        {
            _isFullscreen = Screen.fullScreen;
            _currentResolution = Screen.width + " x " + Screen.height;
            
            if(PlayerPrefs.HasKey(PlayerPrefsSettingsConsts.FULLSCREEN_TOGGLE))
            {
                _isFullscreen = PlayerPrefs.GetInt(PlayerPrefsSettingsConsts.FULLSCREEN_TOGGLE) == 1 ? true : false;
                _currentResolution = PlayerPrefs.GetString(PlayerPrefsSettingsConsts.SCREEN_RESOLUTION);
            }
        }

        private void InitializeToggle()
        {
            _fullScreenToggle.isOn = Screen.fullScreen;
            _fullScreenToggle.onValueChanged.AddListener(HandleFullscreenToggleChanged);
        }

        private void HandleFullscreenToggleChanged(bool p_toggle)
        {
            if(_isFullscreen != p_toggle || _currentResolution != _resolutionMultiOption.GetCurrentOption())
                OptionsChanged(true);
            else
                OptionsChanged(false);
        }

        private void InitializeMultiOption()
        {
            string[] __availableResolutions = new string[Screen.resolutions.Length];
            
            for (int i = 0; i < Screen.resolutions.Length; i++)
                __availableResolutions[i] = Screen.resolutions[i].width + " x " + Screen.resolutions[i].height;

            _resolutionMultiOption.InitializeOptions(Screen.width + " x " + Screen.height, __availableResolutions);

            _resolutionMultiOption.onOptionChanged = HandleMultiOptionChanged;
        }

        private void HandleMultiOptionChanged()
        {
            if(_currentResolution != _resolutionMultiOption.GetCurrentOption() || _isFullscreen != Screen.fullScreen)
                OptionsChanged(true);
            else
                OptionsChanged(false);
        }

        private void OptionsChanged(bool p_apply)
        {
            _applyReturnButton.onClick.RemoveAllListeners();

            if(p_apply)
            {
                _applyReturnText.text = "APPLY";
                _applyReturnButton.onClick.AddListener(PressedApply);
            }
            else
            {
                _applyReturnText.text = "RETURN";
                _applyReturnButton.onClick.AddListener(_menuSceneController.OpenOptions);
            }
        }

        private void PressedApply()
        {
            // pops up confirmation screen
                // on 'yes' callback, apply screen settings and save on playerprefs, then openoptions
                // on 'no' callback, revert fullscreentoggle and resolution
        }
    }
}
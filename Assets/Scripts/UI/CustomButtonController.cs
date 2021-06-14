using GameManagers;
using UnityEngine;
using Utilities.Audio;

namespace UI.CustomButtonController
{
    public class CustomButtonController : MonoBehaviour
    {
        public void HandleButtonHighlight()
        {
            AudioManager.instance.Play(AudioNameEnum.UI_BUTTON_HIGHLIGHTED);
        }

        public void HandleButtonSelected()
        {
            AudioManager.instance.Play(AudioNameEnum.UI_BUTTON_HIGHLIGHTED);
        }

        public void HandleButtonPressed()
        {
            AudioManager.instance.Play(AudioNameEnum.UI_BUTTON_PRESSED);
        }
    }
}

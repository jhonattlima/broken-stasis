using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Audio;

namespace GameManagers
{
    public class CustomSceneManager : MonoBehaviour
    {
        public static CustomSceneManager instance;
        private GameObject _lastUISelected;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            AudioManager.instance.FadeOut(AudioNameEnum.SOUND_TRACK_INTRO, 1, delegate ()
            {
                AudioManager.instance.Play(AudioNameEnum.SOUND_TRACK_GAMEPLAY, true);
            });

            DontDestroyOnLoad(instance);

            _lastUISelected = new GameObject();
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(_lastUISelected);
            }
            else
            {
                _lastUISelected = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}

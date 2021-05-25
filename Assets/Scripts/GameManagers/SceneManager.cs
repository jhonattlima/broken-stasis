using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Audio;

namespace GameManagers
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager instance;
        private GameObject _lastUISelected;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            AudioManager.instance.Play(AudioNameEnum.SOUND_TRACK, true);

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

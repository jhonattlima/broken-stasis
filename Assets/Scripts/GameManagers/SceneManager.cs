using Audio;
using UnityEngine;

namespace GameManager
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            AudioManager.instance.Play(AudioNameEnum.SOUND_TRACK, true);

            DontDestroyOnLoad(instance);
        }
    }
}

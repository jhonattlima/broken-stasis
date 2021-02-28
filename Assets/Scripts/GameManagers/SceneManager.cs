using UnityEngine;
using Utilities.Audio;

namespace GameManagers
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

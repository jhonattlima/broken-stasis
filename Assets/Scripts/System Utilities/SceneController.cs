using Audio;
using UnityEngine;

namespace Utilities
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController instance;

        private void Awake()
        {
            if(instance == null)
                instance = this;

            AudioManager.instance.Play(AudioNameEnum.SOUND_TRACK, true);

            DontDestroyOnLoad(instance);
        }
    }
}

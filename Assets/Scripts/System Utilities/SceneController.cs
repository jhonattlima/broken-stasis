using Audio;
using GameManagers;
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

            CreateGameEventManager();
        }

        private void CreateGameEventManager()
        {
            GameEventManager.SetGameEvents(ChapterManager.instance.GetGameEvents());
        }
    }
}

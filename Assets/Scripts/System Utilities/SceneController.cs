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

            DontDestroyOnLoad(instance);
        }
    }
}

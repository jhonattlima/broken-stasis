using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioClipParam")]
    public class AudioClipParams : ScriptableObject
    {
        public AudioClip audioFile;
        [Range(0.0f, 1.0f)]
        public float volume;
    }
}
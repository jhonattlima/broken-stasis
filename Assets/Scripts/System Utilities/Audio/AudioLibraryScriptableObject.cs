using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioLibrary")]
    public class AudioLibraryScriptableObject : ScriptableObject
    {
        public AudioClipUnit[] AudioLibrary;
    }
}
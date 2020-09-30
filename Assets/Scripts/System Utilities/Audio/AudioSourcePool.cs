using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSourcePool
    {
        private List<AudioSource> _sourcesPool = new List<AudioSource>();
        private Transform _poolParent;
        private GameObject _audiosourcePool;

        public AudioSourcePool(GameObject p_audioSourcePool, Transform p_poolParent)
        {
            _poolParent = p_poolParent;

            InstantiateNewAudioSource();
        }

        private AudioSource InstantiateNewAudioSource()
        {
            GameObject __newAudioSourceGameObject = MonoBehaviour.Instantiate(new GameObject("AudioSource " + _sourcesPool.Count + 1), _poolParent);

            __newAudioSourceGameObject.AddComponent<AudioSource>();

            _sourcesPool.Add(__newAudioSourceGameObject.GetComponent<AudioSource>());

            return _sourcesPool[_sourcesPool.Count - 1];
        }

        public AudioSource GetFreeAudioSource()
        {
            foreach (AudioSource __audioSource in _sourcesPool)
            {
                if (!__audioSource.isPlaying)
                    return __audioSource;
            }

            return InstantiateNewAudioSource();
        }

    }
}

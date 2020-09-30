using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSourcePool
    {
        private List<AudioSource> _sourcesPool = new List<AudioSource>();
        private Transform _poolParent;

        public AudioSourcePool(Transform p_poolParent)
        {
            _poolParent = p_poolParent;

            InstantiateNewAudioSource();
        }

        private AudioSource InstantiateNewAudioSource()
        {
            GameObject __newAudioSourceGameObject = new GameObject("AudioSource " + _sourcesPool.Count + 1);
            __newAudioSourceGameObject.transform.SetParent(_poolParent);

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

        public AudioSource GetAudioWithClip(AudioClip p_audioClip)
        {
            foreach (AudioSource __audioSource in _sourcesPool)
            {
                if (__audioSource.clip == p_audioClip)
                    return __audioSource;
            }

            return null;
        }
        

    }
}

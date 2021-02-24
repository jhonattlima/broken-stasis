using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
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

        public List<AudioSource> GetAudiosWithClip(AudioClip p_audioClip)
        {
            List<AudioSource> __audioSources = new List<AudioSource>();

            foreach (AudioSource __audioSource in _sourcesPool)
            {
                if (__audioSource.clip == p_audioClip)
                    __audioSources.Add(__audioSource);
            }

            return __audioSources;
        }
    }
}

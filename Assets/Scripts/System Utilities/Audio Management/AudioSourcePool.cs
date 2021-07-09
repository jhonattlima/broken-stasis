using System.Collections.Generic;
using GameManagers;
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
        }

        private AudioSource InstantiateNewAudioSource()
        {
            GameObject __newAudioSourceGameObject = new GameObject("AudioSource " + _sourcesPool.Count);
            __newAudioSourceGameObject.transform.SetParent(_poolParent);

            var __audiosource = __newAudioSourceGameObject.AddComponent<AudioSource>();
            __audiosource.playOnAwake = false;

            _sourcesPool.Add(__audiosource);

            return __audiosource;
        }

        public AudioSource GetFreeAudioSource(List<AudioSource> _pausedAudioSources)
        {
            foreach (AudioSource __audioSource in _sourcesPool)
            {
                if (!__audioSource.isPlaying && !_pausedAudioSources.Contains(__audioSource))
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

        public List<AudioSource> GetAllAudioSources()
        {
            return _sourcesPool;
        }
    }
}

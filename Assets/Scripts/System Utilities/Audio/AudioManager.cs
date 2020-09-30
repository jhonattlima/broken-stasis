﻿using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioLibraryScriptableObject _audioLibrary;

        private static AudioManager _instance;
        private static GameObject _audioManagerGameObject;
        private static AudioSourcePool _audioSourcePool;
        
        public static AudioManager instance
        {
            get
            {
                if (_instance == null)
                {
                    InitializeAudioManager();
                }
                return _instance;
            }
        }

        private static void InitializeAudioManager()
        {
            _audioLibrary = Resources.Load<AudioLibraryScriptableObject>("AudioLibrary");

            _audioManagerGameObject = new GameObject("AudioManager");
            _audioManagerGameObject.AddComponent<AudioManager>();

            if (_audioSourcePool == null)
                // _audioSourcePool = new AudioSourcePool(
                //     Instantiate(new GameObject("AudioSourcePool"), _audioManagerGameObject.gameObject.transform).transform);

            _instance = _audioManagerGameObject.GetComponent<AudioManager>();
            DontDestroyOnLoad(_audioManagerGameObject);
        }

        public void Play(AudioNameEnum p_audio, bool p_loop)
        {
            AudioSource __audioSource = _audioSourcePool.GetFreeAudioSource();

            AudioClipParams __audioClipParams = _audioLibrary.AudioLibrary.Find(clip => clip.audioName.Equals(p_audio.ToString())).audioClipParams;
            
            if(!__audioClipParams) 
            {
                Debug.LogError("Audio manager: audioclip not found: " + p_audio.ToString());
                return;
            }
            
            __audioSource.loop = p_loop;
            __audioSource.clip = __audioClipParams.audioFile;
            __audioSource.volume = __audioClipParams.volume;

            __audioSource.Play();
        }
    }
}

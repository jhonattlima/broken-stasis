using System;
using UnityEngine;
using Utilities;
using Utilities.Audio;
using Utilities.VariableManagement;

namespace GameManagers
{
    public enum AudioRange
    {
        LOW = 8,
        MEDIUM = 15,
        HIGH = 20
    }

    public class AudioManager : MonoBehaviour
    {
        private static AudioLibraryScriptableObject _audioLibrary;

        private static AudioManager _instance;
        private static GameObject _audioManagerGameObject;
        private static GameObject _audioSourcePoolGameObject;
        private static AudioSourcePool _audioSourcePool;
        private static AudioLibraryPopulator _audioLibraryPopulator;

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
            _audioLibraryPopulator = new AudioLibraryPopulator();
            _audioLibraryPopulator.InitializeAudioLibrary();

            _audioLibrary = Resources.Load<AudioLibraryScriptableObject>("AudioLibrary");

            _audioManagerGameObject = new GameObject("AudioManager");
            _audioManagerGameObject.AddComponent<AudioManager>();

            if (_audioSourcePool == null)
                _audioSourcePoolGameObject = new GameObject("AudioSourcePool");

            _audioSourcePool = new AudioSourcePool(_audioSourcePoolGameObject.transform);
            _audioSourcePoolGameObject.transform.SetParent(_audioManagerGameObject.transform);

            _instance = _audioManagerGameObject.GetComponent<AudioManager>();
            DontDestroyOnLoad(_audioManagerGameObject);
        }

        public AudioSource Play(AudioNameEnum p_audio, bool p_loop = false, Action p_onAudioEnd = null)
        {
            AudioSource __audioSource = _audioSourcePool.GetFreeAudioSource();

            AudioClipParams __audioClipParams = _audioLibrary.AudioLibrary.Find(clip => clip.audioName.Equals(p_audio.ToString())).audioClipParams;

            if (!__audioClipParams)
            {
                Debug.LogError("Audio manager: audioclip not found: " + p_audio.ToString());
                return null;
            }

            __audioSource.loop = p_loop;
            __audioSource.clip = __audioClipParams.audioFile;
            __audioSource.volume = __audioClipParams.volume;
            __audioSource.spatialBlend = 0f;

            __audioSource.Play();

            if (p_onAudioEnd != null)
                TFWToolKit.Timer(__audioSource.clip.length, p_onAudioEnd);

            return __audioSource;
        }

        public AudioSource PlayAtPosition(AudioNameEnum p_audio, Vector3 p_position, bool p_loop = false, AudioRange p_audioRange = AudioRange.HIGH)
        {
            AudioSource __audioSource = _audioSourcePool.GetFreeAudioSource();
            __audioSource.gameObject.transform.position = p_position;

            AudioClipParams __audioClipParams = _audioLibrary.AudioLibrary.Find(clip => clip.audioName.Equals(p_audio.ToString())).audioClipParams;

            if (!__audioClipParams)
            {
                Debug.LogError("Audio manager: audioclip not found: " + p_audio.ToString());
                return null;
            }

            __audioSource.loop = p_loop;
            __audioSource.clip = __audioClipParams.audioFile;
            __audioSource.volume = __audioClipParams.volume;

            __audioSource.spatialBlend = 1f;
            __audioSource.rolloffMode = UnityEngine.AudioRolloffMode.Custom;
            __audioSource.maxDistance = (int) p_audioRange;

            __audioSource.Play();

            return __audioSource;
        }

        public void Stop(AudioNameEnum p_audio)
        {
            AudioClip __clip = _audioLibrary.AudioLibrary.Find(clip => clip.audioName.Equals(p_audio.ToString())).audioClipParams.audioFile;

            if (__clip != null)
            {
                foreach (AudioSource __audioSource in _audioSourcePool.GetAudiosWithClip(__clip))
                    __audioSource?.Stop();
            }
        }

        public void Pause(AudioNameEnum p_audio)
        {
            AudioClip __clip = _audioLibrary.AudioLibrary.Find(clip => clip.audioName.Equals(p_audio.ToString())).audioClipParams.audioFile;

            if (__clip != null)
            {
                foreach (AudioSource __audioSource in _audioSourcePool.GetAudiosWithClip(__clip))
                {
                    __audioSource?.Stop();

                    if (__audioSource != null)
                    {
                        if (__audioSource.isPlaying)
                            __audioSource.Pause();
                        else
                            __audioSource.Play();
                    }
                }
            }
        }
    }
}

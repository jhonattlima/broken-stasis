using System;
using UnityEditor;
using UnityEngine;

namespace Audio
{
    public class AudioLibraryPopulator
    {
        private static AudioLibraryScriptableObject _audioLibrary;

        private const string AUDIO_CLIP_PARAM_SCRIPT_OBJECT_PATH = "AudioClipParamAssets/";
        private const string AUDIO_LIB_SCRIPT_OBJECT_PATH = "Assets/Scripts/System Utilities/Audio Management/Resources/AudioLibrary.asset";
        private const string AUDIO_CLIP_PARAM_ASSET_PATH = "Assets/Scripts/System Utilities/Audio Management/Resources/AudioClipParams/";

        public void InitializeAudioLibrary()
        {
            PopulateAudioAssets();
            LoadLibrary();
            RemoveMissingAudios();
            BuildLibrary();
        }

        private static void LoadLibrary()
        {
            _audioLibrary = Resources.Load<AudioLibraryScriptableObject>("AudioLibrary");

            if (_audioLibrary == null)
            {
                _audioLibrary = new AudioLibraryScriptableObject();
                AssetDatabase.CreateAsset(_audioLibrary, AUDIO_LIB_SCRIPT_OBJECT_PATH);
            }
        }

        private void PopulateAudioAssets()
        {
            foreach (var __audioNameEnum in Enum.GetValues(typeof(AudioNameEnum)))
            {
                if (!AudioParamExists(__audioNameEnum.ToString()))
                    CreateAudioParam(__audioNameEnum.ToString());
            }
        }

        private void BuildLibrary()
        {
            foreach (var __audioNameEnum in Enum.GetValues(typeof(AudioNameEnum)))
            {
                AudioClipUnit __audioClipUnit = new AudioClipUnit()
                {
                    audioName = __audioNameEnum.ToString(),
                    audioClipParams = Resources.Load<AudioClipParams>(AUDIO_CLIP_PARAM_SCRIPT_OBJECT_PATH + __audioNameEnum.ToString())
                };

                AddAudioClipsToLibrary(__audioNameEnum, __audioClipUnit);
            }
        }

        private static void AddAudioClipsToLibrary(object __audioNameEnum, AudioClipUnit __audioClipUnit)
        {
            bool __shouldAddNewEntry = true;
            foreach (var __audioClipUnitInLib in _audioLibrary.AudioLibrary)
            {
                if (__audioClipUnitInLib.audioName == __audioNameEnum.ToString())
                    __shouldAddNewEntry = false;
            }

            if (__shouldAddNewEntry)
            {
                _audioLibrary.AudioLibrary.Add(__audioClipUnit);
            }
            else
            {
                _audioLibrary.AudioLibrary[_audioLibrary.AudioLibrary.FindIndex(__index => __index.audioName.Equals(__audioNameEnum.ToString()))] = __audioClipUnit;
            }
        }

        private void RemoveMissingAudios()
        {
            foreach (var __audioClipUnitInLib in _audioLibrary.AudioLibrary)
            {
                if (!Enum.IsDefined(typeof(AudioNameEnum), __audioClipUnitInLib.audioName.ToString()))
                {
                    AssetDatabase.DeleteAsset(AUDIO_CLIP_PARAM_ASSET_PATH + __audioClipUnitInLib.audioName + ".asset");

                    _audioLibrary.AudioLibrary.Remove(__audioClipUnitInLib);
                }
            }
        }

        private bool AudioParamExists(string p_audioName)
        {
            return Resources.Load<AudioClipParams>(AUDIO_CLIP_PARAM_SCRIPT_OBJECT_PATH + p_audioName) != null;
        }

        private void CreateAudioParam(string p_audioName)
        {
            AudioClipParams __newAudioClipUnit = new AudioClipParams();
            AssetDatabase.CreateAsset(__newAudioClipUnit, AUDIO_CLIP_PARAM_ASSET_PATH + p_audioName + ".asset");
        }
    }
}
using System;
using UnityEditor;
using UnityEngine;

namespace Audio
{
    public class AudioLibraryPopulator
    {
        public void PopulateAudioAssets()
        {
            foreach(var __audioNameEnum in Enum.GetValues(typeof(AudioNameEnum)))
            {
                Debug.Log("Audio name " + __audioNameEnum.ToString() + " Exists? " + AudioParamExists(__audioNameEnum.ToString()));
                if(!AudioParamExists(__audioNameEnum.ToString()))
                    CreateAudioParam(__audioNameEnum.ToString());
            }
        }

        private bool AudioParamExists(string p_audioName)
        {
            return Resources.Load<AudioClipParams>("AudioClipParams/"+p_audioName) != null;
        }

        private void CreateAudioParam(string p_audioName)
        {
            AudioClipParams __newAudioClipUnit = new AudioClipParams();
            AssetDatabase.CreateAsset(__newAudioClipUnit, "Assets/Scripts/System Utilities/Audio/Resources/AudioClipParams/"+p_audioName+".asset");
        }
    }
}
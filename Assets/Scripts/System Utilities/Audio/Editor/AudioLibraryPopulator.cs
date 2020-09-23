using System;
using UnityEditor;
using UnityEngine;

namespace Audio
{
    public class AudioLibraryPopulator
    {
        private string _path;

        public AudioLibraryPopulator()
        {
            _path = AssetDatabase.GetAssetPath(Resources.Load<GameObject>("AudioClipParams/AudioClipPathReference"));
        }   
        public void PopulateAudioAssets()
        {
            foreach(var __audioNameEnum in Enum.GetValues(typeof(AudioNameEnum)))
            {
                if(!AudioParamExists(__audioNameEnum.ToString()))
                    CreateAudioParam(__audioNameEnum.ToString());
            }
        }

        private bool AudioParamExists(string p_audioName)
        {
            return false;
        }

        private void CreateAudioParam(string p_audioName)
        {
            AudioLibraryScriptableObject __newAudioClipUnit = new AudioLibraryScriptableObject();
            AssetDatabase.CreateAsset(__newAudioClipUnit, "Assets/Scripts/System Utilities/Audio/Resources/AudioClipParams/"+p_audioName+".mat");
        }
    }
}
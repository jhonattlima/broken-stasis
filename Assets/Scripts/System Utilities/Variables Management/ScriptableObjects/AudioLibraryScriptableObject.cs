using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VariableManagement
{
    public enum AudioName
    {
        BASHER_FOOTSTEPS,
        DOOR_OPEN,
        AUDIO_NOVO
    }

    [System.Serializable]
    public class AudioClipUnit
    {
        public AudioName audioName;
        public AudioClip audioFile;
        public float volume;
    }

    [CreateAssetMenu(fileName = "AudioLibrary")]
    public class AudioLibraryScriptableObject : ScriptableObject
    {
        public string steamID;
        public string xboxID;
        public string ps4ID;        
    }
}
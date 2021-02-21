using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
 
    // [CreateAssetMenu(fileName = "AudioVariables")]   
    public class AudioLibraryScriptableObject : ScriptableObject
    {
        public List<AudioClipUnit> AudioLibrary;
    }
}
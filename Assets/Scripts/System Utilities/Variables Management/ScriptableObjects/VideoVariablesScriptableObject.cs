using UnityEngine;
using UnityEngine.Video;

namespace Utilities.VariableManagement
{
    [CreateAssetMenu(fileName = "VideoVariables")]
    public class VideoVariablesScriptableObject : ScriptableObject
    {
        public VideoClip splinterEscape;
        public float splinterEscapeVolume;
        public VideoClip endCredits;
        public float endCreditsVolume;
        public VideoClip studioIntro;
        public float studioIntroVolume;
    }   
}
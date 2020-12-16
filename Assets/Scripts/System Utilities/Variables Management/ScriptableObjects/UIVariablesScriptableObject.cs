using UnityEngine;

namespace VariableManagement
{
    // [CreateAssetMenu(fileName = "UIVariables")]
    public class UIVariablesScriptableObject : ScriptableObject
    {
        public float defaultNotificationDuration;
        public int defaultReadingWPM;
    }
}
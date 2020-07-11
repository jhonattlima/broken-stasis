using UnityEngine;

namespace VariableManagement
{
    // [CreateAssetMenu(fileName = "PlayerVariables")]
    public class PlayerVariablesScriptableObject : ScriptableObject
    {
        public float regularSpeed;
        public float backwardSpeedMultiplier;
        public float runningSpeedMultiplier;
    }   
}

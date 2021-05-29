using UnityEngine;

namespace Utilities.VariableManagement
{
    //[CreateAssetMenu(fileName = "GameplayVariables")]
    public class GameplayVariablesScriptableObject : ScriptableObject
    {
        public string cutSceneSplinterProjectVideoPath;
        public string cutSceneCreditsProjectVideoPath;
        public float threeLocksDoorLockLoadingTimeInSeconds;
    }   
}
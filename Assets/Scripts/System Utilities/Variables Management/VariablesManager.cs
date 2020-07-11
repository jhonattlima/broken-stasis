
using UnityEngine;

namespace VariableManagement
{
    public static class VariablesManager
    {
        private static PlayerVariablesScriptableObject _playerVariables;
        public static PlayerVariablesScriptableObject playerVariables 
        {
            get
            {
                if(_playerVariables == null) _playerVariables = Resources.Load<PlayerVariablesScriptableObject>("Variables/PlayerVariables");

                return _playerVariables;
            }
        }

        private static CameraVariablesScriptableObject _cameraVariables;
        public static CameraVariablesScriptableObject cameraVariables 
        {
            get
            {
                if(_cameraVariables == null) _cameraVariables = Resources.Load<CameraVariablesScriptableObject>("Variables/CameraVariables");

                return _cameraVariables;
            }
        }
    }
}
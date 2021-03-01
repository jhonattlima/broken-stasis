﻿
using UnityEngine;

namespace Utilities.VariableManagement
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

        private static UIVariablesScriptableObject _uiVariables;
        public static UIVariablesScriptableObject uiVariables 
        {
            get
            {
                if(_uiVariables == null) _uiVariables = Resources.Load<UIVariablesScriptableObject>("Variables/UIVariables");

                return _uiVariables;
            }
        }
    }
}
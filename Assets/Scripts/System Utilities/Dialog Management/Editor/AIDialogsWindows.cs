using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utilities.UI;
using Utilities.VariableManagement;

namespace Utilities.Audio
{
    public class AIDialogsWindows : EditorWindow
    {
        private static AIDialogsWindows _window;
        private static AIDialogScriptableObject _dialogsLibraryAsset;
        private static List<DialogTextConversation> _listDialogConversations;
        
        //TODO: CRIAR AIDialogLibraryPopulator
        // private static AudioLibraryPopulator _audioLibraryPopulator;

        private static Vector2 _scrollPosition;
        private static bool[] _collapseState;

        [MenuItem("TFW Tools/Load AI Dialogs")]
        public static void Initialize()
        {
            if (_window == null)
                _window = GetWindow();

            // _audioLibraryPopulator = new AudioLibraryPopulator();
            // _audioLibraryPopulator.InitializeAudioLibrary();

            LoadDialogsLibrary();
        }

        public static AIDialogsWindows GetWindow()
        {
            return (AIDialogsWindows)GetWindow(typeof(AIDialogsWindows), false, "AI Dialogs");
        }

        public void OnGUI()
        {
            if (_listDialogConversations == null || _listDialogConversations.Count != Enum.GetValues(typeof(DialogEnum)).Length)
                LoadDialogsLibrary();

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            {
                if (_listDialogConversations != null && _listDialogConversations.Count > 0)
                {
        //TODO: CONTINUAR A PARTIR DAQUI
                    DrawDialogsLibraryList();
                }
            }
            GUILayout.EndScrollView();
        }

        private static void LoadDialogsLibrary()
        {
            _dialogsLibraryAsset = Resources.Load<AIDialogScriptableObject>("AI Dialogs");

            _listAudioClips = _audioLibraryAsset.AudioLibrary;
            _listAudioClips.Sort((a, b) => a.audioName.CompareTo(b.audioName));

            _collapseState = new bool[_listAudioClips.Count];
        }

        private void DrawAudioLibraryList()
        {
            for (int i = 0; i < _listAudioClips.Count; i++)
            {
                _collapseState[i] = EditorGUILayout.Foldout(_collapseState[i], _listAudioClips[i].audioName.ToString());

                if (_collapseState[i])
                {
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    {
                        Editor.CreateEditor(_listAudioClips[i].audioClipParams).OnInspectorGUI();
                    }
                    GUILayout.EndVertical();
                }
            }
        }

        public void OnInspectorUpdate()
        {
            this.Repaint();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Audio
{
    public class AudioLibraryWindow : EditorWindow
    {
        private static AudioLibraryWindow _window;
        private static AudioLibraryScriptableObject _audioLibraryAsset;
        private static List<AudioClipUnit> _listAudioClips;
        private static AudioLibraryPopulator _audioLibraryPopulator;

        private static Vector2 _scrollPosition;

        [MenuItem("TFW Tools/Audio Library")]
        public static void Initialize()
        {
            if (_window == null)
                _window = GetWindow();

            UnityEditor.EditorApplication.projectChanged -= LoadAudioLibrary;
            UnityEditor.EditorApplication.projectChanged += LoadAudioLibrary;

            _audioLibraryPopulator = new AudioLibraryPopulator();
            
            _audioLibraryPopulator.PopulateAudioAssets();
        }

        public static AudioLibraryWindow GetWindow()
        {
            return (AudioLibraryWindow)GetWindow(typeof(AudioLibraryWindow), false, "Audio Library");
        }

        public void OnGUI()
        {
            if (_listAudioClips == null)
                LoadAudioLibrary();

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            {
                if (_listAudioClips != null && _listAudioClips.Count > 0)
                {
                    DrawAudioLibraryList();
                }
            }
            GUILayout.EndScrollView();
        }

        private static void LoadAudioLibrary()
        {
            _audioLibraryAsset = Resources.Load<AudioLibraryScriptableObject>("AudioLibrary");

            _listAudioClips = _audioLibraryAsset.AudioLibrary.ToList();
        }

        private void DrawAudioLibraryList()
        {
            for (int i = 0; i < _listAudioClips.Count; i++)
            {
                // GUILayout.BeginHorizontal();
                // {
                //     GUILayout.Label(_listAudioClips[i].audioName.ToString());
                // }
                // GUILayout.EndHorizontal();

                // GUILayout.BeginVertical(EditorStyles.helpBox);
                // {
                //     Editor.CreateEditor(_listAudioClips[i].audioClipParams).OnInspectorGUI();
                // }
                // GUILayout.EndVertical();
            }
        }
    }
}
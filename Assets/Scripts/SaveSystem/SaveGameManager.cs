using System.IO;
using UnityEditor;
using UnityEngine;
using GameManagers;

namespace SaveSystem
{
    public static class SaveGameManager
    {
        public static GameSaveData gameSaveData;

        private const string FILE_PATH = "/Savedata";
        private const string FILE_NAME = "/SaveFile.jua";

        public static void SaveGame()
        {
            string __saveDataJson = JsonUtility.ToJson(gameSaveData, true);
            byte[] __bytes = System.Text.Encoding.UTF8.GetBytes(__saveDataJson);

            if (!Directory.Exists(Application.dataPath + FILE_PATH))
                Directory.CreateDirectory(Application.dataPath + FILE_PATH);

            File.WriteAllBytes(Application.dataPath + FILE_PATH + FILE_NAME, __bytes);
        }

        public static bool HasLoadFile()
        {
            return File.Exists(Application.dataPath + FILE_PATH + FILE_NAME);
        }

        public static void NewGame()
        {
            gameSaveData = new GameSaveData();
        }

        public static void LoadGame()
        {
            byte[] __bytes = File.ReadAllBytes(Application.dataPath + FILE_PATH + FILE_NAME);
            string __saveDataJson = System.Text.Encoding.UTF8.GetString(__bytes); ;

            gameSaveData = JsonUtility.FromJson<GameSaveData>(__saveDataJson);
        }

        [MenuItem("TFW Tools/Utilities/Clear Save Data")]
        public static void ClearSaveGame()
        {
            gameSaveData = new GameSaveData();

            EditorUtility.DisplayDialog("Save Game Cleared", "Deleted Save Game at " + Application.dataPath + FILE_PATH + FILE_NAME, "OK");

            if (File.Exists(Application.dataPath + FILE_PATH + FILE_NAME))
                File.Delete(Application.dataPath + FILE_PATH + FILE_NAME);
        }
    }
}

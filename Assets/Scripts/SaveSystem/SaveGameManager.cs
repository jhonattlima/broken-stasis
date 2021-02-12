using System.IO;
using UnityEditor;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveGameManager
    {
        public static GameSaveData gameSaveData;
        private const string FILE_PATH = "/SaveData/SaveFile.jua";

        public static void SaveGame()
        {
            string __saveDataJson = JsonUtility.ToJson(gameSaveData, true);
            byte[] __bytes = System.Text.Encoding.UTF8.GetBytes(__saveDataJson);

            File.WriteAllBytes(Application.dataPath + FILE_PATH, __bytes);
        }

        public static void LoadGame()
        {
            byte[] __bytes = File.ReadAllBytes(Application.dataPath + FILE_PATH);
            string __saveDataJson = System.Text.Encoding.UTF8.GetString(__bytes);;

            gameSaveData = JsonUtility.FromJson<GameSaveData>(__saveDataJson);
        }

        [MenuItem("TFW Tools/Utilities/Clear Save Data")]
        public static void ClearSaveGame()
        {
            gameSaveData = new GameSaveData();
            File.Delete(Application.dataPath + FILE_PATH);
        }
    }
}

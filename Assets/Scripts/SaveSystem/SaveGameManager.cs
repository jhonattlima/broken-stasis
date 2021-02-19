using System.IO;
using UnityEditor;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveGameManager
    {
        public static GameSaveData gameSaveData;

        private const string FILE_PATH = "/Savedata";
        private const string FILE_NAME = "/SaveFile.jua";

        public static void SaveGame()
        {
            Debug.Log(gameSaveData.doorsList.ToString());
            Debug.Log(gameSaveData.doorsList.Count);
            string __saveDataJson = JsonUtility.ToJson(gameSaveData, true);
            byte[] __bytes = System.Text.Encoding.UTF8.GetBytes(__saveDataJson);

            if (!Directory.Exists(Application.dataPath + FILE_PATH))
                Directory.CreateDirectory(Application.dataPath + FILE_PATH);

            File.WriteAllBytes(Application.dataPath + FILE_PATH + FILE_NAME, __bytes);
        }

        public static bool LoadGame()
        {
            if(!File.Exists(Application.dataPath + FILE_PATH + FILE_NAME))
            {
                if(gameSaveData == null)
                    gameSaveData = new GameSaveData();
                
                return false;   
            }

            byte[] __bytes = File.ReadAllBytes(Application.dataPath + FILE_PATH + FILE_NAME);
            string __saveDataJson = System.Text.Encoding.UTF8.GetString(__bytes); ;

            gameSaveData = JsonUtility.FromJson<GameSaveData>(__saveDataJson);

            return true;
        }

        [MenuItem("TFW Tools/Utilities/Clear Save Data")]
        public static void ClearSaveGame()
        {
            gameSaveData = new GameSaveData();

            if(File.Exists(Application.dataPath + FILE_PATH + FILE_NAME))
                File.Delete(Application.dataPath + FILE_PATH + FILE_NAME);
        }
    }
}

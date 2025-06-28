using System.IO;
using _01Script.Save;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01Script.Manager
{
    public class SaveManager : MonoBehaviour
    {
        [Header("Set LoadFileName")]
        [SerializeField] private int loadFileName;
        [Header("Show Data")]
        [SerializeField]private GameSaveData data; //저장되는 곳
        private string _fileName = "SaveData";
        private string _gameSaveFilePath;
        
        private int _saveIndex; //저장 순서

        [ContextMenu("ResetData")]
        private void ResetData() //정보 초기화
        { 
            PlayerPrefs.SetString(_fileName, "");
        }

        [ContextMenu("LoadData")]
        private void LoadData() //받아오기
        {
            if (File.Exists($"{_gameSaveFilePath}/{loadFileName}"))
            {
                string saveData = File.ReadAllText($"{_gameSaveFilePath}/{loadFileName}");
                data = JsonUtility.FromJson<GameSaveData>(saveData);
                SaveData();
            }
        }

        [ContextMenu("NewData")]
        private void NewData() //새걸로
        {
            data = new GameSaveData();
        }
        
        [ContextMenu("File")]
        private void FileSave() //파일로
        {
            string saveData = SaveData();

            if (!Directory.Exists(_gameSaveFilePath))
            {
                Directory.CreateDirectory(_gameSaveFilePath);
            }
            
            File.WriteAllText($"{_gameSaveFilePath}/{_saveIndex}", saveData);
            
            PlayerPrefs.SetInt("saveIndex",++_saveIndex);
            PlayerPrefs.Save();
        }
        private void Awake()
        {
            _gameSaveFilePath = Application.persistentDataPath + $"/{_fileName}";
            if (PlayerPrefs.GetString(_fileName) != "") //안 비어 있다면
            {
                print("저장용 스크립트를 불러 왔습니다.");
                string json = PlayerPrefs.GetString(_fileName);
                data = JsonUtility.FromJson<GameSaveData>(json);
            }
            else
            {
                print("저장용 스크립트를 생성 했습니다.");
                data = new GameSaveData();
            }
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private string SaveData() //저장
        {
            print("저장용 스크립트를 저장 했습니다.");
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(_fileName, json);
            PlayerPrefs.Save();
            return json;
        }
    }
}
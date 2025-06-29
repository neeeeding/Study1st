using System;
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
        [SerializeField]private int saveIndex; //저장 순서
        
        private string _fileName = "SaveData";
        private string _gameSaveFilePath; //파일 위치

        [ContextMenu("ResetData")]
        private void ResetData() //데이터 초기화
        { 
            print($"저장된 모든 데이터를 삭제 합니다.");
            if (Directory.Exists(_gameSaveFilePath))
            {
                Directory.Delete(_gameSaveFilePath, true);
            }
            
            PlayerPrefs.SetInt("saveIndex",0);
            saveIndex = 0;
            
            PlayerPrefs.SetString(_fileName, "");
            data = new GameSaveData();
        }

        [ContextMenu("LoadData")]
        private void LoadData() //받아오기
        {
            if (File.Exists($"{_gameSaveFilePath}/{loadFileName}"))
            {
                print($"파일 {loadFileName} 를 로드하는 데 성공 했습니다.");
                string saveData = File.ReadAllText($"{_gameSaveFilePath}/{loadFileName}");
                data = JsonUtility.FromJson<GameSaveData>(saveData);
                SaveData();
            }
            else
            {
                print($"파일 {loadFileName} 는 존재 하지 않습니다.");
            }
        }

        
        [ContextMenu("NewData")]
        private void NewData() //새걸로
        {
            print("데이터를 새 것으로 교체 했습니다.");
            data = new GameSaveData();
        }
        
        
        [ContextMenu("FileSave")]
        private void FileSave() //파일로
        {
            string saveData = SaveData();

            if (!Directory.Exists(_gameSaveFilePath))
            {
                print("폴더를 만들었습니다.");
                Directory.CreateDirectory(_gameSaveFilePath);
            }
            print("데이터를 파일로 만들었습니다.");
            
            File.WriteAllText($"{_gameSaveFilePath}/{saveIndex}", saveData);
            
            PlayerPrefs.SetInt("saveIndex",++saveIndex);
            PlayerPrefs.Save();
        }
        private void Awake()
        {
            saveIndex = PlayerPrefs.GetInt("saveIndex");
            
            _gameSaveFilePath = Application.persistentDataPath + $"/{_fileName}";
            if (PlayerPrefs.GetString(_fileName) != "") //안 비어 있다면
            {
                print("데이터를 불러 왔습니다.");
                string json = PlayerPrefs.GetString(_fileName);
                data = JsonUtility.FromJson<GameSaveData>(json);
            }
            else
            {
                print("데이터를 생성 했습니다.");
                data = new GameSaveData();
            }
        }

        private void OnApplicationQuit()
        {
            data.finalDate = DateTime.Now.ToString("yy년 MM월 dd일 tt HH시 mm분"); //마지막 시간 입력
            SaveData();
        }

        private string SaveData() //저장
        {
            print("데이터를 저장 했습니다.");
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(_fileName, json);
            PlayerPrefs.Save();
            return json;
        }
    }
}
using System;
using _01Script.Save;
using UnityEngine;

namespace _01Script.Manager
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField]private GameSaveData _data; //저장되는 곳
        private string _fileName = "SaveData";

        [ContextMenu("R")]
        private void R()
        { 
            PlayerPrefs.SetString(_fileName, "");
        }
        private void Awake()
        {
            if (PlayerPrefs.GetString(_fileName) != "") //안 비어 있다면
            {
                print("저장용 스크립트를 불러 왔습니다.");
                string json = PlayerPrefs.GetString(_fileName);
                _data = JsonUtility.FromJson<GameSaveData>(json);
            }
            else
            {
                print("저장용 스크립트를 생성 했습니다.");
                _data = new GameSaveData();
            }
        }

        private void OnApplicationQuit()
        {
            print("저장용 스크립트를 저장 했습니다.");
            string json = JsonUtility.ToJson(_data);
            PlayerPrefs.SetString(_fileName, json);
            PlayerPrefs.Save();
        }
    }
}
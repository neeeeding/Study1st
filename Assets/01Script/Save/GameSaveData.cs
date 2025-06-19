namespace _01Script.Save
{
    [System.Serializable]
    public class GameSaveData
    {
        public int coin; //소지금
        public SaveDictionary<CarType, bool> playerCar; //소유 차들
        public string finalDate; //마지막 사용 날짜
    }

    public enum CarType
    {
        Red, Blue, Green, Yellow, White
    }
}

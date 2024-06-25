using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private const string SAVE = "#SAVE-VALUE#";
    public static void Save()
    {
        int sCoin = data.sumCoin + GameManager.Instances.coin;
        if (GameManager.Instances.score > data.hightScore)
        {
            data.hightScore = GameManager.Instances.score;
        }
        string[] saveData = new string[]
        {
            ""+sCoin,
            ""+data.hightScore
        };
        string saveString = string.Join(SAVE, saveData);
        File.WriteAllText(Application.dataPath+"/save.txt",saveString);
    }

    public static void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
        string[] Data = saveString.Split(new[] { SAVE }, System.StringSplitOptions.None);
        data.hightScore = int.Parse(Data[1]);
        data.sumCoin = int.Parse(Data[0]);
        GameManager.Instances.sumCoin = int.Parse(Data[0]);
        GameManager.Instances.hightScore = data.hightScore;
    }

    public class data {
        public static int sumCoin;
        public static int hightScore;
    }
}

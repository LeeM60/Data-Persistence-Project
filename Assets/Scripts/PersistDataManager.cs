using System.IO;
using TMPro;
using UnityEngine;

public class PersistDataManager : MonoBehaviour
{
    // Start and Update methods deleted

    //Add a getter to make this a Property (read only) and a private setter so it can be set from within this class but nowhere else
    //It’s encapsulated to only accept modifications from its own class, safe from misuse and corruption from the outside world!
    public static PersistDataManager Instance { get; private set; }

    public string playersName;
    public int playersScore;
    public string hiScorePlayerName;
    //public int hiScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadName();
    }


    [System.Serializable]
    class SaveData
    {
        public string savedPlayerName;
        public int savedPlayersScore;
        //public string savedHiScorePlayerName;
        //public int savedHiScore;
    }

    public void SaveName()
    {
        //Debug.Log("SaveName called: playersName = " + playersName);
        //Debug.Log("SaveName called: savedPlayersScore = " + playersScore);
        //Debug.Log("SaveName called: savedHiScore = " + hiScore);

        SaveData data = new SaveData();
        data.savedPlayerName = playersName;
        data.savedPlayersScore = playersScore;
        //data.savedHiScorePlayerName = hiScorePlayerName;
        //data.savedHiScore = hiScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/datapersistenceproject.json", json);
    }

    public void LoadName()
    {
        //Debug.Log("LoadName called:");

        string path = Application.persistentDataPath + "/datapersistenceproject.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //playersName = data.savedPlayerName;
            playersScore = data.savedPlayersScore;
            hiScorePlayerName = data.savedPlayerName;

            //Debug.Log("LoadName data = playersName = " + playersName);
            //Debug.Log("LoadName data = playersScore = " + playersScore);
            //Debug.Log("LoadName data = hiScore = " + hiScore);
        }
    }

    public void DeleteHiScores()
    {
        string path = Application.persistentDataPath + "/datapersistenceproject.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

}

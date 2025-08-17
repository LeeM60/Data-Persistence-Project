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
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.savedPlayerName = playersName;
        data.savedPlayersScore = playersScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/datapersistenceproject.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/datapersistenceproject.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playersScore = data.savedPlayersScore;
            hiScorePlayerName = data.savedPlayerName;

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

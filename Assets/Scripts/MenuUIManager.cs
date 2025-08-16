using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    
    public TMP_Text thePlayerName;
    public TMP_InputField inputField;
    public TMP_Text hiScoreText;

    private void Start()
    {
        //Debug.Log("Start called:");
        PersistDataManager.Instance.LoadName();//Refresh the data, it might have been updated if a new hiscore was made

        int hiScore = PersistDataManager.Instance.playersScore;
        hiScoreText.text = "1. " + PersistDataManager.Instance.hiScorePlayerName + " " + hiScore.ToString();
        //string name = PersistDataManager.Instance.playersName;
        //inputField.text = PersistDataManager.Instance.playersName;
    }

    public void StartNew()
    {
        //Ensure there's some text in the name box
        if (inputField.text.Length >= 1)
        {
            //Debug.Log("text.length = " + inputField.text.Length);
            SceneManager.LoadScene(1);
        }
    }

    public void TextEntered(string text)
    {
        thePlayerName.text = text;
        PersistDataManager.Instance.playersName = thePlayerName.text;
        //Debug.Log("TextEntered: PersistDataManager.Instance.playersName = " + thisPlayerName.text);
    }    

    public void Exit()
    {
        //PersistDataManager.Instance.SaveName();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void DeleteHiScores()
    {
        PersistDataManager.Instance.DeleteHiScores();
        PersistDataManager.Instance.playersName = "";
        PersistDataManager.Instance.hiScorePlayerName = "";
        PersistDataManager.Instance.playersScore = 0;
        hiScoreText.text = "1. Name - 0";
    }    
}

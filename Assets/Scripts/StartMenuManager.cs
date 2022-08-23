using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenuManager : MonoBehaviour
{
    public static StartMenuManager Instance;
    public string playerName;
    private string dataPath;
    private BestNameScore currBestNameScore;

    private void Start()
    {
        dataPath= Application.persistentDataPath + "/best.json";

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadMain(TMP_InputField nameField)
    {
        Instance.playerName = nameField.text;
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class BestNameScore
    {
        public string name;
        public int score;
    }

    public string GetBestName()
    {
        return StartMenuManager.Instance.currBestNameScore.name;
    }
    public int GetBestScore()
    {
        return StartMenuManager.Instance.currBestNameScore.score;
    }

    public void ReplaceBestIfBetter(string name, int score)
    {
        if(currBestNameScore.score < score)
        {
            currBestNameScore.score= score;
            currBestNameScore.name = name;
            Save();
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(currBestNameScore);
        File.WriteAllText(dataPath,json);
        
    }
    public void Load()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            currBestNameScore = JsonUtility.FromJson<BestNameScore>(json);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); 
#else
        Application.Quit();
#endif
    }

}

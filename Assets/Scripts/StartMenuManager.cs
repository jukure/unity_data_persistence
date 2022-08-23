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
    public BestNameScore currBestNameScore;

    private void Awake()
    {
        dataPath= Application.persistentDataPath + "/best.json";

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Load();
    }
    public void LoadMain(TMP_InputField nameField)
    {
        Instance.playerName = nameField.text;
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    public class BestNameScore
    {
        public string name;
        public int score;
    }

    public string GetBestName()
    {
        return (currBestNameScore.name != null) ? currBestNameScore.name : "";
    }
    public int GetBestScore()
    {
        return currBestNameScore.score;
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

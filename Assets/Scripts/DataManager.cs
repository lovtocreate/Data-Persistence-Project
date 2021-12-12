using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string PlayerName;
    public int Difficulty;
    public string HighScoreText1;
    public string HighScoreText2;
    public string HighScoreText3;
    public int HighScore1;
    public int HighScore2;
    public int HighScore3;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScores();
    }

    [System.Serializable]
    class SaveData
    {
        public int Highscore1;
        public int Highscore2;
        public int Highscore3;
        public string HighScoreText1;
        public string HighScoreText2;
        public string HighScoreText3;
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.Highscore1 = HighScore1;
        data.Highscore2 = HighScore2;
        data.Highscore3 = HighScore3;
        data.HighScoreText1 = HighScoreText1;
        data.HighScoreText2 = HighScoreText2;
        data.HighScoreText3 = HighScoreText3;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore1 = data.Highscore1;
            HighScore2 = data.Highscore2;
            HighScore3 = data.Highscore3;
            HighScoreText1 = data.HighScoreText1;
            HighScoreText2 = data.HighScoreText2;
            HighScoreText3 = data.HighScoreText3;
        }
    }
}


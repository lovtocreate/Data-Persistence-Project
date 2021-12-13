using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if (DataManager.Instance.HighScoreText1 != null)
        {
            BestScoreText.text = "Best Score : " + DataManager.Instance.HighScoreText1;
        }

        ScoreText.text = DataManager.Instance.PlayerName + "'s score " + m_Points;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                if (DataManager.Instance.Difficulty == 1)
                {
                    Ball.AddForce(forceDir, ForceMode.VelocityChange);
                }

                if (DataManager.Instance.Difficulty == 2)
                {
                    Ball.AddForce(forceDir * 2, ForceMode.VelocityChange);
                }

                if (DataManager.Instance.Difficulty == 3)
                {
                    Ball.AddForce(forceDir * 4, ForceMode.VelocityChange);
                }
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        //ScoreText.text = $"Score : {m_Points}";
        ScoreText.text = DataManager.Instance.PlayerName + "'s score " + m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (m_Points > DataManager.Instance.HighScore3)
        {
            DataManager.Instance.HighScore3 = m_Points;
            DataManager.Instance.HighScoreText3 = DataManager.Instance.PlayerName + " : " + m_Points;
        }

        if (m_Points > DataManager.Instance.HighScore2)
        {
            DataManager.Instance.HighScore3 = DataManager.Instance.HighScore2;
            DataManager.Instance.HighScoreText3 = DataManager.Instance.HighScoreText2;
            DataManager.Instance.HighScore2 = m_Points;
            DataManager.Instance.HighScoreText2 = DataManager.Instance.PlayerName + " : " + m_Points;
        }

        if (m_Points > DataManager.Instance.HighScore1)
        {
            DataManager.Instance.HighScore2 = DataManager.Instance.HighScore1;
            DataManager.Instance.HighScoreText2 = DataManager.Instance.HighScoreText1;
            DataManager.Instance.HighScore1 = m_Points;
            DataManager.Instance.HighScoreText1 = DataManager.Instance.PlayerName + " : " + m_Points;
        }

        if (DataManager.Instance.HighScoreText1 != null)
        {
            BestScoreText.text = "Best Score : " + DataManager.Instance.HighScoreText1;
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}

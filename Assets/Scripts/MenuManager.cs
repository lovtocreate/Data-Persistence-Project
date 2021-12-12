using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField name;
    public TMP_Text highScoreText1;
    public TMP_Text highScoreText2;
    public TMP_Text highScoreText3;
    public TMP_Text enterNameText;

    private void Start()
    {
        if (highScoreText1 != null)
        {
            highScoreText1.text = DataManager.Instance.HighScoreText1;
            highScoreText2.text = DataManager.Instance.HighScoreText2;
            highScoreText3.text = DataManager.Instance.HighScoreText3;
        }
    }
    public void LoadMainScene()
    {
        if (name.text == "" && DataManager.Instance.PlayerName == "")
        {
            StartCoroutine("FlashEnterName");
        }

        else if(DataManager.Instance.PlayerName != "" && name.text == "")
        {
            SceneManager.LoadScene(1);
        }

        else if (DataManager.Instance.PlayerName != "" && name.text != "")
        {
            DataManager.Instance.PlayerName = name.text;
            SceneManager.LoadScene(1);
        }

        else
        {
            DataManager.Instance.PlayerName = name.text;
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator FlashEnterName()
    {
        enterNameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        enterNameText.gameObject.SetActive(false);
    }

    public void QuitApp()
    {
        DataManager.Instance.SaveScores();
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void SetDifficulty(int difficulty)
    {
        DataManager.Instance.Difficulty = difficulty;
    }
}
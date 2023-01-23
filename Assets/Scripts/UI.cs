using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text scoreUI;
    public Text endingScoreUI;
    public Text waitingHighscoreUI;
    public GameObject waitingScreen;
    public GameObject endingScreen;
    public GameObject soundOff;

    private void Awake()
    {
        waitingHighscoreUI.text = "Highscore : " + PlayerPrefs.GetInt("highscore", 0).ToString();
    }

    private void Update()
    {
        if (AudioListener.volume == 0)
        {
            soundOff.SetActive(true);
        }
        else
        {
            soundOff.SetActive(false);
        }
    }

    public void UpdateScoreText(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void CloseWaitingPanel()
    {
        waitingScreen.SetActive(false);
    }

    public void OpenEndingPanel(int score)
    {
        endingScreen.SetActive(true);

        endingScoreUI.text = "Your score is : <color=#D41733>" + score.ToString() + "</color>";
    }
}

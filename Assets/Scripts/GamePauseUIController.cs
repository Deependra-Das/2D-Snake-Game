using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GamePauseUIController : MonoBehaviour
{
    public Button ResumeButton;
    public Button BackButton;
    public TextMeshProUGUI HighScoreText;
    public GameManager gameManagerObject;
    private void Awake()
    {
        ResumeButton.onClick.AddListener(OnResumeButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
        RefreshHighScore();
    }

    private void OnResumeButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RefreshHighScore()
    {
        switch (gameManagerObject.GetPlayerCount())
        {
            case 1:
                HighScoreText.text = "SinglePlayer HighScore : " +HighScoreManager.Instance.GetHighestScore("SinglePlayer").ToString();
                break;

            case 2:
                HighScoreText.text = "Co-Op Mode HighScore : " + HighScoreManager.Instance.GetHighestScore("CoOpMode").ToString();
                break;

        }

    }

}

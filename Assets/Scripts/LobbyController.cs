using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button SinglePlayerButton;
    public Button CoOpModeButton;
    public Button QuitGameButton;

    public TextMeshProUGUI SinglePlayerHighScoreText;
    public TextMeshProUGUI CoOpModeHighScoreText;
    private void Start()
    {
        SinglePlayerButton.onClick.AddListener(OnSinglePlayerButtonClick);
        CoOpModeButton.onClick.AddListener(OnCoOpModeButtonClick);
        QuitGameButton.onClick.AddListener(OnQuitGameButtonClick);
        AudioManager.Instance.PlayBGM(AudioTypeList.backgroundMusic);
        RefreshHighScore();
    }

    private void OnSinglePlayerButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        SceneManager.LoadScene(1);
    }

    private void OnCoOpModeButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        SceneManager.LoadScene(2);
    }

    private void OnQuitGameButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        Application.Quit();
    }
    public void RefreshHighScore()
    {
        SinglePlayerHighScoreText.text = HighScoreManager.Instance.GetHighestScore("SinglePlayer").ToString();
        CoOpModeHighScoreText.text = HighScoreManager.Instance.GetHighestScore("CoOpMode").ToString();
    }

}

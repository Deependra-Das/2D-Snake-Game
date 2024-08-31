using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUIController : MonoBehaviour
{
    public Button RestartButton;
    public Button BackButton;

    public GameUIManager gameUIManagerObject;
    public TextMeshProUGUI WinnerScoreText;

    private void Start()
    {
        RestartButton.onClick.AddListener(OnRestartButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnRestartButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        SceneManager.LoadScene(0);
    }

    public void setWinnerScore(SnakeID snakeID)
    {
        if (gameUIManagerObject.GetPlayerCount()==1)
        {
          WinnerScoreText.text="Your Score : "+gameUIManagerObject.GetPowerupItem(SnakeID.SNAKE_P1).scoreValue;
        }

        if (gameUIManagerObject.GetPlayerCount() == 2)
        {
           if(snakeID==SnakeID.SNAKE_P1)
            {
                WinnerScoreText.text = "Player 2 Won";
            }
            else if (snakeID == SnakeID.SNAKE_P2)
            {
                WinnerScoreText.text = "Player 1 Won";
            }
        }

    }
}

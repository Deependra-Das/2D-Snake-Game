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

    public GameManager gameManagerObject;
    public TextMeshProUGUI[] ScoreTextList;

    private void Awake()
    {
        RestartButton.onClick.AddListener(OnRestartButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnRestartButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SetWinnerScore(SnakeID snakeID, bool headToHeadCollision)
    {
        if (gameManagerObject.GetPlayerCount()==1)
        {
            ScoreTextList[0].text="Your Score : "+ gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue;
            ScoreTextList[1].text = "";
            HighScoreManager.Instance.SetHighestScore("SinglePlayer", gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue);
            ScoreTextList[2].text = "SinglePlayer HighScore : " + HighScoreManager.Instance.GetHighestScore("SinglePlayer").ToString();
        }

        if (gameManagerObject.GetPlayerCount() == 2)
        {
            if(headToHeadCollision)
            {
                if(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue > gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue)
                {
                    ScoreTextList[0].text = "Player 1 Won with Score: "+ gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue;
                    ScoreTextList[1].text = "Player 2 Lost with Score: " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue;
                    HighScoreManager.Instance.SetHighestScore("CoOpMode", gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue);
           
                  
                }
                else if (gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue < gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue)
                {
                    ScoreTextList[0].text = "Player 2 Won with Score: " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue;
                    ScoreTextList[1].text = "Player 1 Lost with Score: " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue;
                    HighScoreManager.Instance.SetHighestScore("CoOpMode", gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue);
                   
                }
                else
                {
                    ScoreTextList[0].text = "Its a Draw with Score : " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue;
                    ScoreTextList[1].text = "";
                    HighScoreManager.Instance.SetHighestScore("CoOpMode", gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue);
                }
            }
            else
            {
                if (snakeID == SnakeID.SNAKE_P1)
                {
                    ScoreTextList[0].text = "Player 2 Won with Score: " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue;
                    ScoreTextList[1].text = "Player 1 Lost with Score: " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue;
                    HighScoreManager.Instance.SetHighestScore("CoOpMode", gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue);

                }
                else if (snakeID == SnakeID.SNAKE_P2)
                {
                    ScoreTextList[0].text = "Player 1 Won with Score: " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue;
                    ScoreTextList[1].text = "Player 2 Lost with Score: " + gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).scoreValue;
                    HighScoreManager.Instance.SetHighestScore("CoOpMode", gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).scoreValue);

                }
            }

            ScoreTextList[2].text = "Co-Op Mode HighScore : " + HighScoreManager.Instance.GetHighestScore("CoOpMode").ToString();

        }

    }

}

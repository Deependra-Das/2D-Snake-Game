using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManagerObject;
    public void UpdateScore(SnakeID snakeID, int scoreIncrementValue)
    {
        PlayerItem scoreItem = gameManagerObject.GetPlayerItem(snakeID);

        scoreItem.scoreValue += scoreIncrementValue;
        RefreshUI(scoreItem);
    }

    private void RefreshUI(PlayerItem scoreItem)
    {
        if (scoreItem.snakeID == SnakeID.SNAKE_P1)
        {
            scoreItem.scoreText.text = "P1 : " + scoreItem.scoreValue.ToString();
        }
        else if (scoreItem.snakeID == SnakeID.SNAKE_P2)
        {
            scoreItem.scoreText.text = scoreItem.scoreValue.ToString() + " : P2 ";
        }

    }

    private void Update()
    {
        UpadateActivePowerup();
    }

    public void UpadateActivePowerup()
    {
        switch (gameManagerObject.GetPlayerCount())
        {
            case 1:
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).ActivePowerupImageList[0].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).snakePrefab.checkShieldStatus());
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).ActivePowerupImageList[1].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).snakePrefab.checkScoreMultiplierStatus());
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).ActivePowerupImageList[2].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).snakePrefab.checkSpeedBoostStatus());
                break;

            case 2:
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).ActivePowerupImageList[0].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).snakePrefab.checkShieldStatus());
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).ActivePowerupImageList[1].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).snakePrefab.checkScoreMultiplierStatus());
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).ActivePowerupImageList[2].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P1).snakePrefab.checkSpeedBoostStatus());

                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).ActivePowerupImageList[0].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).snakePrefab.checkShieldStatus());
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).ActivePowerupImageList[1].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).snakePrefab.checkScoreMultiplierStatus());
                gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).ActivePowerupImageList[2].gameObject.SetActive(gameManagerObject.GetPlayerItem(SnakeID.SNAKE_P2).snakePrefab.checkSpeedBoostStatus());
                break;

        }
    }

    public void SetScoreToZero(SnakeID snakeID)
    {
        PlayerItem scoreItem = gameManagerObject.GetPlayerItem(snakeID);

        scoreItem.scoreValue = 0;
        RefreshUI(scoreItem);
    }


}



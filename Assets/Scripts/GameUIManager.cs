using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public PlayerScoreItem[] playerScores;

    public void UpdateScore(SnakeID snakeID, int scoreIncrementValue)
    {
        PlayerScoreItem scoreItem = GetPowerupItem(snakeID);

        scoreItem.scoreValue += scoreIncrementValue;
        RefreshUI(scoreItem);
    }

    private void RefreshUI(PlayerScoreItem scoreItem)
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
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i].ActivePowerupImageList[0].gameObject.SetActive(playerScores[i].snakePrefab.checkShieldStatus());
            playerScores[i].ActivePowerupImageList[1].gameObject.SetActive(playerScores[i].snakePrefab.checkScoreMultiplierStatus());
            playerScores[i].ActivePowerupImageList[2].gameObject.SetActive(playerScores[i].snakePrefab.checkSpeedBoostStatus());
        }

    }

    public PlayerScoreItem GetPowerupItem(SnakeID snakeID)
    {
        PlayerScoreItem item = Array.Find(playerScores, item => item.snakeID == snakeID);
        if (item != null)
        {
            return item;
        }
        return null;
    }

    public int GetPlayerCount()
    {
        return playerScores.Length;
    }

    public void KillAllPlayers()
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
           playerScores[i].snakePrefab.enabled = false;
        }
    }


}


[Serializable]
public class PlayerScoreItem
{
    public SnakeID snakeID;
    public SnakeController snakePrefab;
    public int scoreValue = 0;
    public TextMeshProUGUI scoreText;
    public CanvasRenderer[] ActivePowerupImageList;
}



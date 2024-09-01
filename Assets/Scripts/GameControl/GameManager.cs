using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerItem[] playerList;

    public PlayerItem GetPlayerItem(SnakeID snakeID)
    {
        PlayerItem item = Array.Find(playerList, item => item.snakeID == snakeID);
        if (item != null)
        {
            return item;
        }
        return null;
    }

    private void Start()
    {
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceBGM, false);
        AudioManager.Instance.PlayBGM(AudioTypeList.backgroundMusic);
    }

    public int GetPlayerCount()
    {
        return playerList.Length;
    }

    public void KillAllPlayers()
    {
        for (int i = 0; i < playerList.Length; i++)
        {
            playerList[i].snakePrefab.enabled = false;
        }
    }
}

[Serializable]
public class PlayerItem
{
    public SnakeID snakeID;
    public SnakeController snakePrefab;
    public int scoreValue = 0;
    public TextMeshProUGUI scoreText;
    public CanvasRenderer[] ActivePowerupImageList;
}

public enum SnakeID
{
    SNAKE_P1,
    SNAKE_P2
}


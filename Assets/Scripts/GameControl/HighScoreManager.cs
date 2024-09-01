using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager instance;
    public static HighScoreManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetHighestScore(string gameMode)
    {
        return PlayerPrefs.GetInt(gameMode, 0);
    }

    public void SetHighestScore(string gameMode, int highScore)
    {
        if (GetHighestScore(gameMode) < highScore)
        {
            PlayerPrefs.SetInt(gameMode, highScore);
        }
    }
}


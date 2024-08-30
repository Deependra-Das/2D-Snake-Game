using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    private int scoreValue = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public SnakeController snakeController;

    public CanvasRenderer[] ActivePowerupImageList;

    public void UpdateScore(int scoreIncrementValue)
    {
        scoreValue += scoreIncrementValue;
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreText.text = "Score: " + scoreValue.ToString();

    }

    private void Update()
    {
        UpadateActivePowerup();
    }

    public void UpadateActivePowerup()
    {
        ActivePowerupImageList[0].gameObject.SetActive(snakeController.checkShieldStatus());
        ActivePowerupImageList[1].gameObject.SetActive(snakeController.checkScoreMultiplierStatus());
        ActivePowerupImageList[2].gameObject.SetActive(snakeController.checkSpeedBoostStatus());
            
    }
}

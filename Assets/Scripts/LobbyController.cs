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

    private void Start()
    {
        SinglePlayerButton.onClick.AddListener(OnSinglePlayerButtonClick);
        CoOpModeButton.onClick.AddListener(OnCoOpModeButtonClick);
    }

    private void OnSinglePlayerButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnCoOpModeButtonClick()
    {
        SceneManager.LoadScene(2);
    }
}

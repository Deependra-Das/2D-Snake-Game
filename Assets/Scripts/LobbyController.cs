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
        QuitGameButton.onClick.AddListener(OnQuitGameButtonClick);
        AudioManager.Instance.PlayBGM(AudioTypeList.backgroundMusic);
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
}

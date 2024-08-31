using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GamePauseUIController : MonoBehaviour
{
    public Button ResumeButton;
    public Button BackButton;

    private void Awake()
    {
        this.gameObject.SetActive(false);
        ResumeButton.onClick.AddListener(OnResumeButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnResumeButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ActivateGamePausePanel()
    {
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
   [SerializeField] private PlayerController playerController;
   [SerializeField] private TextMeshProUGUI _fireBallCount;
   [SerializeField] private TextMeshProUGUI _blindCount;
   [SerializeField] private GameObject _pausePanel;
   [SerializeField] private GameObject _defeatPanel;
   [SerializeField] private PlayerController _playerController;
    private void Start()
    {
        Time.timeScale = 1.0f;
        AudioListener.volume = 1;
    }

    private void Update()
    {
        _fireBallCount.text = "FIRE BALL : " + playerController.fireBallCount;
        _blindCount.text = "BLIND : " + playerController.blindCount;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_pausePanel.activeInHierarchy)
            {
                _pausePanel.SetActive(true);
                Time.timeScale = 0;
                AudioListener.volume = 0;
            }
            else if(_pausePanel.activeInHierarchy) 
            {
                Time.timeScale = 1;
                _pausePanel.SetActive(false);
                AudioListener.volume = 1;
            }
        }
        if(_playerController.hpPlayer <=0)
        {
            Invoke(nameof(EnablingDefeatPanel),2f);
        }
    }
    public void HomeButton()
    {
        Chest_Handler.ClearStaticBool();
        SceneManager.LoadScene(0);
    }
    public void PlayAgain()
    {
        Chest_Handler.ClearStaticBool();
        SceneManager.LoadScene(1);
    }
    public void ResumeButton()
    {
        _pausePanel.SetActive(false);
        AudioListener.volume = 1;
        Time.timeScale = 1;
    }
    private void EnablingDefeatPanel()
    {
        _defeatPanel.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _exitPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioSource _clickSound;
    private void Start()
    {
        Time.timeScale = 1f;
        _volumeSlider.value = PlayerPrefs.GetFloat("VolumeFloat");
    }
    private void Update()
    {
        AudioListener.volume = _volumeSlider.value;
    }
    public void StartGame()
    {
        PlayingClickSound();
        PlayerPrefs.SetFloat("VolumeFloat", _volumeSlider.value);
        Invoke(nameof(_startingGame), 0.6f);
        
    }
    public void QuitButton()
    {
        PlayingClickSound();
        _exitPanel.SetActive(true);
    }
    public void CancelQuit()
    {
        PlayingClickSound();
        _exitPanel.SetActive(false);
    }
    public void QuitGame()
    {
        PlayingClickSound();
        PlayerPrefs.SetFloat("VolumeFloat",_volumeSlider.value);
        Application.Quit();
    }
    public void EnablingOptions()
    {
        PlayingClickSound();
        _optionPanel.SetActive(true);
    }
    public void CancelOptions()
    {
        PlayingClickSound();
       _optionPanel.SetActive(false);
    }
    private void PlayingClickSound()
    {
        _clickSound.Play();
    }
    private void _startingGame()
    {
        SceneManager.LoadScene(2);
    }
}

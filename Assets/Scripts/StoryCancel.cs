using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryCancel : MonoBehaviour
{
    private int _noOfClicks;
    [SerializeField] private GameObject _story;
    [SerializeField]  private GameObject _controls;
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _noOfClicks++;
        }
        if(_noOfClicks==1)
        {
            _story.SetActive(false);
            _controls.SetActive(true);
        }
        if(_noOfClicks >= 2)
        {
            SceneManager.LoadScene(1);
        }
    }
}

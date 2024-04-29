using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellDownFinder : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _losingPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _player)
        {
            _losingPanel.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}

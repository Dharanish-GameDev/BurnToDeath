using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackChest : MonoBehaviour
{
    [SerializeField] private KeyHandler _keyHandler;
    [SerializeField] private GameObject  _karin01;
    [SerializeField] private bool _karinDestroyed = false;
    public GameObject _EtoOpen;
    [SerializeField] private bool buttonPressed;
    [SerializeField] private bool canOpen;
    [SerializeField] private Chest_Handler _chestHandler;
    private bool enabledKarin;
    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Player") && _keyHandler.hasBlackkey && _karinDestroyed)
        {
            buttonPressed = true;
            if (canOpen)
            {
                _EtoOpen.GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = true;
                _chestHandler.darkChestOpened = true;
                 canOpen = false;
            }
        }
    }
    private void Update()
    {
        if(SecondChestTrigger.canOnKarin && !enabledKarin)
        {
            _karin01.SetActive(true);
            enabledKarin = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && buttonPressed)
        {
            canOpen = true;
        }
        if(_karin01 == null && enabledKarin)
        {
            _karinDestroyed = true;
        }

    }
}

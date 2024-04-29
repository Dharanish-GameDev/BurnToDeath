using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrownChest : MonoBehaviour
{
    [SerializeField] private KeyHandler _keyHandler;
    [SerializeField] private GameObject _boar;
    [SerializeField] private bool bothBoarsDestroyed = false;
    public GameObject _EtoOpen;
    [SerializeField]  private bool buttonPressed;
    [SerializeField] private bool canOpen;
    [SerializeField] private Chest_Handler _chestHandler;
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player") && _keyHandler.hasBrownKey && bothBoarsDestroyed)
        {
            buttonPressed = true;
            if(canOpen)
            {
                _EtoOpen.GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = true;
                _chestHandler.brownChestOpened = true;
                canOpen = false;
            }
        }
    }
    private void Update()
    {
        if(_keyHandler.hasBrownKey && _boar == null)
        {
            bothBoarsDestroyed = true;
        }
        if(Input.GetKeyDown(KeyCode.E) && buttonPressed)
        {
            canOpen = true;
        }

    }

}

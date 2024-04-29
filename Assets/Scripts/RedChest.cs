using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedChest : MonoBehaviour
{
    [SerializeField] private KeyHandler _keyHandler;
    [SerializeField] private GameObject _boss;
    public bool _bossDestroyed = false;
    public GameObject _EtoOpen;
    [SerializeField] private bool buttonPressed;
    [SerializeField] private bool canOpen;
    [SerializeField] private Chest_Handler _chestHandler;
    private bool enabledBoss;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _keyHandler.hasRedkey && _bossDestroyed)
        {
            buttonPressed = true;
            if (canOpen)
            {
                _EtoOpen.GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = true;
                _chestHandler.redChestOpened = true;
                canOpen = false;
            }
        }
    }
    private void Update()
    {
        if (FourthChestTrigger.canOnBoss && !enabledBoss)
        {
            _boss.SetActive(true);
            enabledBoss = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && buttonPressed)
        {
            canOpen = true;
        }
        if (_boss == null && enabledBoss)
        {
            _bossDestroyed = true;
        }
    }
}

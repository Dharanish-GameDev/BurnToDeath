using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueChest : MonoBehaviour
{
    [SerializeField] private KeyHandler _keyHandler;
    [SerializeField] private GameObject _karin02;
    [SerializeField] private bool _karinDestroyed = false;
    public GameObject _EtoOpen;
    [SerializeField] private bool buttonPressed;
    [SerializeField] private bool canOpen;
    [SerializeField] private Chest_Handler _chestHandler;
    private bool enabledKarin;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _keyHandler.hasBluekey && _karinDestroyed)
        {
            buttonPressed = true;
            if (canOpen)
            {
                _EtoOpen.GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = true;
                _chestHandler.blueChestOpened = true;
                canOpen = false;
            }
        }
    }
    private void Update()
    {
        if (ThirdChestTrigger.canOnKarin && !enabledKarin)
        {
            _karin02.SetActive(true);
            enabledKarin = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && buttonPressed)
        {
            canOpen = true;
        }
        if (_karin02 == null && enabledKarin)
        {
            _karinDestroyed = true;
        }
    }
}

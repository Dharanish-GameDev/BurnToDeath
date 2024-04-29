using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brown_Key : MonoBehaviour
{
    [SerializeField] private KeyHandler _keyHandler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _keyHandler.hasBrownKey = true;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}

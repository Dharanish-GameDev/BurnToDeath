using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarinIdleCheck : MonoBehaviour
{
    public static bool karinCanIdle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) karinCanIdle = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) karinCanIdle = false;
    }
}
        

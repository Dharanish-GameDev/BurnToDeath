using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_Dedector : MonoBehaviour
{
    public bool LadderDedected;
    private Rigidbody2D _playerRb;
    private void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        _playerRb= temp.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
            LadderDedected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            LadderDedected = false;
            Animator tempAni= _playerRb.GetComponent<Animator>();
            tempAni.SetBool("LadderIdle", false);
            PlayerController._isClimbingLadder = false;
            _playerRb.gravityScale = 0.2f;
            Invoke(nameof(GravityReset),0.2f);
        }
           
    }
    private void GravityReset()
    {
         _playerRb.gravityScale = 1;
    }
}

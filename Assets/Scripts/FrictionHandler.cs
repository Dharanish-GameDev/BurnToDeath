using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FrictionHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
     public PlayerController playerController;
     private float _temp;

     private void Start()
     {
         _temp = playerController.jumpForce;
     }

     private void OnTriggerStay2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Player"))
         {
             playerController.jumpForce = 10f;
             playerRb.drag = 5;
         }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Player"))
         {
             playerController.jumpForce = _temp;
             playerRb.drag = 0;
         }
     }
}

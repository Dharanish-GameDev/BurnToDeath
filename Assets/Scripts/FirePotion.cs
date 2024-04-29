using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirePotion : MonoBehaviour
{
    public static bool canUseFireBall;
    public float potionLifeTime;
    public bool isAdded;
    [SerializeField] TextMeshProUGUI _collectedTextPotion;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isAdded)
        {
            canUseFireBall = true;
            var _playerController = other.GetComponent<PlayerController>();
            _playerController.fireBallCount += 1;
            _playerController.blindCount += 1;
            isAdded = true;
        }
        if(isAdded)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(EnablingPotionText());
        }
    }
    IEnumerator EnablingPotionText()
    {
        _collectedTextPotion.enabled = true;
        yield return new WaitForSeconds(1.2f);
        _collectedTextPotion.enabled = false;
        this.gameObject.SetActive(false);
    }

   

    
}

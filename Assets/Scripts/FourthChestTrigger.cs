using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthChestTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public static bool canOnBoss;

    private void Awake()
    {
        canOnBoss = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            canOnBoss = true;
        }
    }
}

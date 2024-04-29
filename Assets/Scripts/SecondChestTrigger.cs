using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChestTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public static bool canOnKarin;

    private void Awake()
    {
        canOnKarin = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _player)
        {
            canOnKarin = true;
        }
    }

    private void OnDisable()
    {
        canOnKarin = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private Transform _currentPoint;
    [SerializeField] private bool isReached;

    private void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _player)
        {
            _player.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == _player)
        {
            _player.transform.SetParent(null,true);
        }
    }
    private void Update()
    {
        if (isReached)
        {
            _currentPoint = _rightPoint;
        }
        else
        {
            _currentPoint = _leftPoint;
        }
       
  
        transform.position = Vector2.MoveTowards(transform.position,_currentPoint.position, 1f *Time.deltaTime);
        if (transform.position == _leftPoint.position)
        {
            isReached = true;
        }
        else if (transform.position == _rightPoint.position)
        {
            isReached = false;
        }

    }
}

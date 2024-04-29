using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFired : MonoBehaviour
{
    private bool _damagedPlayer;
    private SpriteRenderer _playerSprite;
    Color _spriteColor = Color.white;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_damagedPlayer)
        {
            GameObject tempObj = GameObject.FindGameObjectWithTag("Player");
            _playerSprite = tempObj.GetComponent<SpriteRenderer>();
            _spriteColor.a = 0.4f;
            tempObj.GetComponent<PlayerController>().hpPlayer -= 10;
            tempObj.GetComponent<SpriteRenderer>().color = _spriteColor;
            StartCoroutine(ColorChanger(_playerSprite));
            _damagedPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))_damagedPlayer = false;
    }
     IEnumerator ColorChanger(SpriteRenderer renderer)
    {
        _spriteColor.a = 0.2f;
        renderer.color = _spriteColor;
        yield return new WaitForSeconds(0.2f);
        _spriteColor.a = 0.8f;
        renderer.color = _spriteColor;
        yield return new WaitForSeconds(0.2f);
        _spriteColor.a = 0.2f;
        renderer.color = _spriteColor;
        yield return new WaitForSeconds(0.2f);
        _spriteColor.a = 1f;
        renderer.color = _spriteColor;
    }

}

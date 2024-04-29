using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingWood : MonoBehaviour
{
    [SerializeField] GameObject _player;
    private Color _spriteColor = Color.white;
    private bool isDamaged;
    void Update()
    {
        transform.Rotate(0, 0, 85 * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _player)
        {
            if (isDamaged) return;
            _player.GetComponent<PlayerController>().hpPlayer -= 10f;
            StartCoroutine(ColorChanger(_player.GetComponent<SpriteRenderer>()));
            isDamaged = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == _player)
        {
            isDamaged = false;
        }
    }
    IEnumerator ColorChanger(SpriteRenderer renderer)
    {
        _spriteColor.a = 0.2f;
        renderer.color = _spriteColor;
        yield return new WaitForSeconds(0.15f);
        _spriteColor.a = 0.8f;
        renderer.color = _spriteColor;
        yield return new WaitForSeconds(0.15f);
        _spriteColor.a = 0.2f;
        renderer.color = _spriteColor;
        yield return new WaitForSeconds(0.15f);
        _spriteColor.a = 1f;
        renderer.color = _spriteColor;
    }
}

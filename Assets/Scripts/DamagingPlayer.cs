
using UnityEngine;
using System.Collections;

public class DamagingPlayer : MonoBehaviour
{
   [SerializeField] private SpriteRenderer _renderer;
   [SerializeField] private PlayerController controller;
    private bool _isDamaged;
    Color _spriteColor= Color.white;
    [SerializeField] private EnemyBoss _enemyBoss;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_enemyBoss.isDead)
        {
            if (_isDamaged) return;
            controller.hpPlayer -= 10;
            StartCoroutine(ColorChanger(_renderer));
            _isDamaged = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isDamaged = false;
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

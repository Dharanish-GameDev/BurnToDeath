using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private Animator _playerAnimator;
    private static int _damageTaken = Animator.StringToHash("DamageTaken");
    private Color _spriteColor = Color.white;
    [SerializeField] private GameObject _karin01;
    [SerializeField] private GameObject _karin02;

    private void OnEnable()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Karin");
        if (temp != null && temp.name == "Karin")
        {
            _karin01 = temp;
        }
        else if (temp != null && temp.name == "Karin (1)")
        {
            _karin02 = temp;
        }
    }

    private void Update()
    {
        Invoke(nameof(Destruct),5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (collision.gameObject.CompareTag("Player"))
        {
            if(_karin01 == null)
            {
                _playerAnimator.gameObject.GetComponent<PlayerController>().hpPlayer -= 25;
            }
            else if(_karin02 == null)
            {
                _playerAnimator.gameObject.GetComponent<PlayerController>().hpPlayer -= 15;
            }
            StartCoroutine(ColorChanger(_playerAnimator.gameObject.GetComponent<SpriteRenderer>()));
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke(nameof(Destruct), 3.15f);

        }
        if (collision.gameObject.CompareTag("Ground")) Destroy(this.gameObject);
    }
    private void Destruct()
    {
        Destroy(this.gameObject);
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

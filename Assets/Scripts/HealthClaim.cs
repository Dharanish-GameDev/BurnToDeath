using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthClaim : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private GameObject _fxHltClaimPrefab;
    [SerializeField] private List<GameObject> _fxHltClaimArray;
    private void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        _player = temp.GetComponent<Transform>();
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)_player.position + new Vector2(0,0.15f), 2.3f * Time.deltaTime);
        if(transform.position.x < _player.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager _healthClaimSfx = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        float hp = _player.GetComponent<PlayerController>().hpPlayer;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GameObject.FindGameObjectWithTag("HealthClaim").GetComponent<SpriteRenderer>().enabled = true;
            if (hp >= 125 && hp <= 150)
            {
                _player.GetComponent<PlayerController>().hpPlayer = 150;
                _healthClaimSfx.PlayingHpClaimSFX();
            }
            else if(hp <125)
            {
                _player.GetComponent<PlayerController>().hpPlayer += 25;
                _healthClaimSfx.PlayingHpClaimSFX();
            }
            StartCoroutine(FxDestroy());
        }
    }
   IEnumerator FxDestroy()
   {
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("HealthClaim").GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject);
   }
}

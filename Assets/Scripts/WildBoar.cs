using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoar : MonoBehaviour
{
    #region Variables
    [SerializeField]private GameObject _player;
    [SerializeField] private bool playerInRange = false;
    [SerializeField] private Transform _brownChest;
    private float _dis;
    private float _boarStartDis;
    private Animator _animator;
    private static readonly int move = Animator.StringToHash("Move");
    [SerializeField] private bool canIdle;
    public  float boarHp;
    public bool isDead = false;
    [SerializeField] private GameObject _hltClaim;
    private bool gaveHealth;
    [SerializeField] private ParticleSystem _deadPart;
    [SerializeField] private Color _spriteColor = Color.white;
    [SerializeField] private bool isBlinded = false;
    [SerializeField] private GameObject _faceBlind;
    [SerializeField] private SpriteRenderer _starSprite;
    #endregion

    private void OnEnable()
    {
        _boarStartDis = transform.position.x;
        _animator = GetComponent<Animator>();
        isDead = false;
    }
    private void Update()
    {
        if (isDead) return;
        PlayerInRangeDedect();
        if (playerInRange && !isBlinded)
        {
            if(_player.transform.position.x < _brownChest.position.x)
            {
                transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, _player.transform.position.x + 0.5f, 1.2f * Time.deltaTime), transform.position.y);
            }

            if(transform.position.x > _player.transform.position.x)
            {
                transform.localEulerAngles = Vector2.zero;
            }
            else
            {
                transform.localEulerAngles = new Vector2(0f, 180f);
            }
            canIdle = false;
        }
        else if (!playerInRange&& !isBlinded)
        {
            transform.position =  new Vector2 (Mathf.MoveTowards(transform.position.x, _brownChest.position.x - 1.5f, 1.2f * Time.deltaTime),transform.position.y);
            transform.localEulerAngles = new Vector2(0f, 180f);
            canIdle = transform.position.x == _brownChest.position.x - 1.5f ? true : false;
        }
       
        _animator.SetBool(move, canIdle ? false : true);
        CheckDead();
        BlindCheck();
    }
    private void PlayerInRangeDedect()
    {
        _dis = transform.position.x - _player.transform.position.x; // distance between player and the boar 
        if (Mathf.Abs(transform.position.x) <= Mathf.Abs(_boarStartDis - 3.18f) || _player.transform.position.x > _brownChest.transform.position.x)
        {
            playerInRange = false;
        }
        if(_player.transform.position.x > _boarStartDis - 3.2f && _player.transform.position.x < _brownChest.transform.position.x)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _player && !isDead && !isBlinded)
        {
            if(this.gameObject.name == "Boar")
            {
                _player.GetComponent<PlayerController>().hpPlayer -= 5;
            }
            StartCoroutine(ColorChanger(_player.GetComponent<SpriteRenderer>()));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == _player && !isDead && !isBlinded)
        {
            _player.GetComponent<PlayerController>().hpPlayer -= 0.4f * Time.deltaTime;
            canIdle = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            canIdle = false;
        }
    }
    private void CheckDead()
    {
        if (boarHp <= 0)
        {
            isDead = true;
            _deadPart.Play();
            _animator.SetBool(move, false);
            GetComponent<SpriteRenderer>().enabled = false;
            _starSprite.enabled= false;
            Invoke(nameof(GivingHealthOrb), 1.5f);
        }
        else
        {
            isDead = false;
        }
    }
    private void GivingHealthOrb()
    {
          Vector2 tempPos = transform.position;
          Quaternion tempRot = transform.position.x < _player.transform.position.x ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
          if (!gaveHealth)
          {
              Instantiate(_hltClaim, tempPos, tempRot);
              gaveHealth = true;
              Destroy(this.gameObject);
              isDead = false;
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
    private void BlindCheck()
    {
        float dis;
        if (BlindManager.canCheck)
        {
            Transform temp = GameObject.FindGameObjectWithTag("BlindDedect").gameObject.transform;
            dis = transform.position.x - temp.position.x;
            if (Mathf.Abs(dis) < 8)
            {
                isBlinded = true;
                _animator.SetBool(move, false);
                if (!_faceBlind.activeInHierarchy)
                {
                    _faceBlind.SetActive(true);
                }
            }
        }
        else
        {
            isBlinded = false;
            if (_faceBlind.activeInHierarchy)
            {
                _faceBlind.SetActive(false);
            }
        }
    }
}
//96.54 // 98.04

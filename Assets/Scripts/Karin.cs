using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Karin : MonoBehaviour
{
    #region Private Variables
    [SerializeField] Transform _shootPoint;
    [SerializeField] GameObject _greenBulletPrebab;
    [SerializeField] float _bulletSpeed;
    [SerializeField] Transform _player;
    [Range(0, 2)]
    [SerializeField] float _moveSpeed;
    private bool facingRight;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private Animator _karinAnimator;
    private static readonly int _attack = Animator.StringToHash("CanMakeAttack");
    private static readonly int _move = Animator.StringToHash("Moving");
    private static readonly int Dead = Animator.StringToHash("Dead");
    [SerializeField] private Ladder_Dedector _ladDedect;
    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _canAttack;
    [SerializeField] private GameObject _hltClaim;
    [SerializeField] private bool gaveHealth;
    private Color _spriteColor = Color.white;
    [SerializeField] private GameObject _faceBlind;
    [SerializeField] private bool isBlinded;
    private int _attackMin;
    private int _attackMax;
    private float _moveOffsetX;
    private float _moveOffsetY;
    #endregion

    #region Public Variables
    public int KarinHP;
    public bool isDead;
    #endregion

    private void OnEnable()
    {
        isDead = false;
        StartCoroutine(PlayAttack());
    }
    private void Start()
    {
       
        
    }

    private void Update()
    {
       Moving();
       Attack();
       BlindCheck();
    }
    public void ShootBullet() // shoots seperate bullet from karin
    {

        bool temp = transform.position.x < _player.transform.position.x ? true : false;
        facingRight = temp ? true : false; // Checking facing right or not to rotate the bullet
        Quaternion angleOfBullet = facingRight ? Quaternion.Euler(0,180,0) : Quaternion.Euler(0,0,0);
        GameObject newBullet = Instantiate(_greenBulletPrebab, _shootPoint.position, angleOfBullet);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-_shootPoint.right * _bulletSpeed, ForceMode2D.Impulse);
        
        _isAttacking = false;
        _canAttack = false;
    }
    IEnumerator PlayAttack()
    {
        int attackCoolDown;
       
        while (!isDead )
        {
            if (this.gameObject.name == "Karin")
            {
                _attackMin = 3;
                _attackMax = 5;
            }
            else if(this.gameObject.name == "Karin (1)")
            {
                _attackMin = 2;
                _attackMax = 4;
            }
            attackCoolDown = Random.Range(_attackMin, _attackMax);
            yield return new WaitForSeconds(attackCoolDown);
            _canAttack = true;
            yield return new WaitForSeconds(1);
            _canAttack = false;
        }
    }
    public void MakingAttackTrue()
    {
        _isAttacking = true;
    }

    private void Moving()
    {
        if (this.gameObject.name == "Karin")
        {
            _moveOffsetX = 0.7f;
            _moveOffsetY = 0f;
        }
        else if (this.gameObject.name == "Karin (1)")
        {
            _moveOffsetX = 0.3f;
            _moveOffsetY = 0.2f;
        }
        transform.localEulerAngles = transform.position.x < _player.position.x && !isBlinded ? new Vector2(0, 180) : new Vector2(0, 0);
        if (!_isAttacking && !PlayerController._isClimbingLadder && !KarinIdleCheck.karinCanIdle && !isDead && !isBlinded)
        {

            transform.position = Vector2.MoveTowards(transform.position, (Vector2)_player.position + new Vector2(_moveOffsetX,_moveOffsetY), _moveSpeed * Time.deltaTime);
            _karinAnimator.SetBool(_move, true);
        }
        else if(KarinIdleCheck.karinCanIdle ||isBlinded) _karinAnimator.SetBool(_move, false);
    }
    private void Attack()
    {
        if(KarinHP <= 0)
        {
            _karinAnimator.SetBool(Dead, true);
            _karinAnimator.SetBool(_move, false);
            isDead = true;
            Invoke(nameof(Death), 1f);
        }
        if(!isBlinded)
        {
            _karinAnimator.SetBool(_attack, _canAttack);
        }
    }
        
    private void Death()
    {
        Vector2 tempPos = transform.position;
        Quaternion tempRot = transform.position.x < _player.transform.position.x ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        if (!gaveHealth)
        {
            Instantiate(_hltClaim, tempPos, tempRot);
            gaveHealth = true;
        }
        Destroy(this.gameObject);
    }
    private void BlindCheck()
    {
        float dis;
        if(BlindManager.canCheck)
        {
            Transform temp = GameObject.FindGameObjectWithTag("BlindDedect").gameObject.transform;
            dis = transform.position.x - temp.position.x;
            if (Mathf.Abs(dis) < 8)
            {
                 isBlinded = true;
                if(!_faceBlind.activeInHierarchy)
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

    // if attacking bool true movement stop and play attackmode animation 
    // after every attack make attacking bool false and enable the movement , wait 10 seconds and stop movement and make attacking bool true
}
   // 1. if blinddedect exist we need to calculate the distance between the blindDedect and the enemy seperately.
   // 2. create a static bool in the BlindDedect and to check if it is actually exist or wot.
   //3. If its true Get the transform here and calculate the distance. If it is within the range then Enable the Head blind and freeze the movement.
   // 4. Once the static bool is false Disable the head Blind And Unfreeze the movemnets.
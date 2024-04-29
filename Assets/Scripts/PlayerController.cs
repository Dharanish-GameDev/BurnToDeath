using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Public Variables
    public float speed;
    public Transform groundCheck;
    public Transform jumpCheck;
    public float move;
    public float checkRadius;
    public float jumpCheckRadius;
    public LayerMask whatIsGrounded;
    public int attackFind;  // 1- FireBallJutsu  , 2- Forward slice, 3 - running slice 
    public GameObject hitCollider;
    public float hpPlayer;
    public float jumpForce;
    public static bool _isClimbingLadder;
    public bool isGrounded;
    public static bool isFacingRight;
    public static Transform rightBlindEnd;
    public static Transform leftBlindEnd;
    public int fireBallCount;
    public int blindCount;
    public static bool obtainedPower;
    #endregion

    #region Private Variables

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _playerSprite;
    private Animator _animator;
    private Vector2 _vecGravity;
    [FormerlySerializedAs("_fallmultipliyer")] [SerializeField] private float fallmultipliyer;
    [FormerlySerializedAs("_jumpTime")] [SerializeField] private float jumpTime;
    [FormerlySerializedAs("_JumpMultiplier")] [SerializeField] private float jumpMultiplier;
    private bool _isJumping;
    private float _jumpCounter;
    private float _firePotionTime;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Slice = Animator.StringToHash("ForwardSlice");
    private static readonly int BallJutsu = Animator.StringToHash("FireBallJutsu");
    private bool _runningSlice;
    private bool _canMove;
    private bool _isMoving;
    private static readonly int Falling = Animator.StringToHash("Falling");
    private static readonly int RunningSlice = Animator.StringToHash("RunningSlice");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsLandedIdle = Animator.StringToHash("IsLandedIdle");
    private static readonly int ClimbingLadder = Animator.StringToHash("ClimbingLadder");
    private static readonly int Blind = Animator.StringToHash("Blinding");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private Vector2 _jumpStartPoint ;
    [SerializeField] private Ladder_Dedector _ladderDedector;
    [SerializeField] private Slider warriorHpSlider;
    [SerializeField] private bool isBlinding;
    [SerializeField] private GameObject _blindBallPrefab;
    [SerializeField] private Transform _blindBallPoint;
    [SerializeField] private Transform _rightBlindEnd;
    [SerializeField] private Transform _leftBlindEnd;
    [SerializeField] private KeyHandler _keyHandler;
    [SerializeField] private bool isDead;
    [SerializeField] private Sprite _lowHpimage;
    [SerializeField] private Sprite _highHpimage;
    [SerializeField] private Image _hpFillImage;
    [SerializeField] private AudioManager _audioManager;
    
    #endregion

    void Start()
    {
        _runningSlice = false;
        _canMove = true;
        _vecGravity = new Vector2(0, -Physics2D.gravity.y);
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        warriorHpSlider.maxValue = hpPlayer;
        isBlinding = false;
        isFacingRight = true;
        isDead = false;
        obtainedPower = false;

        rightBlindEnd = _rightBlindEnd.transform;  // Getting transform to move the blind towards it.
        leftBlindEnd = _leftBlindEnd.transform;
    }

    void Update()
    {
        if(isDead || obtainedPower) return;
        if(!_ladderDedector.LadderDedected)
        {
            Jumping();
        }
        ForwardSlice();
        FireBallJutsu();
        HPslider();
        BlindingEnemies();
        DeathCheck();
    }
    private void FixedUpdate()
    {    if (isDead || obtainedPower) return;
        isGrounded = CanEndJump();
        move = Input.GetAxis("Horizontal");
        if (_canMove && !isBlinding)
        {
            _rigidbody.velocity = new Vector2(move * speed, _rigidbody.velocity.y);  // setting input for horizontal movement
        }
        FlipCharacter();
        if (move == 0)
        {
            if(_animator.GetBool(IsJumping)) return;
            _animator.SetBool(Idle,true);
            _runningSlice = false;
        }
        if (move > 0) _runningSlice = true;
        _rigidbody.velocity = isBlinding ? new Vector2(0,0) : _rigidbody.velocity;
        LadderClimbing();
    }
    private void Jumping()
    {
        if (_ladderDedector.LadderDedected) return;
        if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W))) && _isGrounded()) // Jump Input
        {
            _rigidbody.velocity = Vector2.up * jumpForce;
            _isJumping = true;
            _jumpCounter = 0;
            _jumpStartPoint = transform.position;
            _animator.SetBool(IsLandedIdle, false);

        }
        if (_rigidbody.velocity.y > 0 && _isJumping == true && !_isClimbingLadder) // Adding extra jump time
        {
            _jumpCounter += Time.deltaTime;
            if (_jumpCounter > jumpTime) _isJumping = false;
            float t = _jumpCounter / jumpTime;
            float currentJump = jumpMultiplier;
            if (t > 0.5f)
            {
                currentJump = jumpMultiplier * (1 - t);
            }

            _rigidbody.velocity += _vecGravity * (currentJump * Time.deltaTime); // Applying gravity for extra jumps
        }

        if (_rigidbody.velocity.y < 0 && !_isClimbingLadder)
        {
            _rigidbody.velocity -= _vecGravity * (fallmultipliyer * Time.deltaTime); // Applying gravity for normal jump
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            _isJumping = false;
            _jumpCounter = 0;
            if (_rigidbody.velocity.y > 0)
            {
                var velocity = _rigidbody.velocity;
                velocity = new Vector2(velocity.x, velocity.y * 0.6f); // Adding gravity for landing fastly
                _rigidbody.velocity = velocity;
            }
        }
        if (_isGrounded())
        {
            _animator.SetBool(Falling, true);
            _animator.SetBool(IsJumping, false);
            _animator.SetBool(IsLandedIdle, true);
        }
        else
        {
            if (CanEndJump())
            {
                _animator.SetBool(Falling, true);
                _animator.SetBool(IsJumping, false);
                _animator.SetBool(IsLandedIdle,true);
            }
            else
            {
                _animator.SetBool(IsJumping, true);
                _animator.SetBool(Falling, false);
                _animator.SetBool(IsLandedIdle, false);
            }
        }
    }
    private void FlipCharacter()
    {
        Vector3 rotation = hitCollider.transform.eulerAngles;
        Vector3 playerRotation = transform.eulerAngles;// Rotating hit collider of the player as per the direction the player in facing
        if (move > 0)
        {
             rotation.y = 0;
             playerRotation.y = 0;
             _animator.SetBool(Idle, false);
            isFacingRight = true;
        }
        else if (move < 0)
        {
            rotation.y = 180f;
            playerRotation.y = 180f;
            _animator.SetBool(Idle, false);
            isFacingRight = false;
        }

        transform.eulerAngles = playerRotation;
        hitCollider.transform.eulerAngles = rotation;
    }

    private void ForwardSlice()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitCollider.SetActive(true);
           
            if (_runningSlice)
            {
                _animator.SetTrigger(RunningSlice);
                _audioManager.PlayingSwordSFX();
                attackFind = 3;
            }
            if(!_runningSlice)
            {
                _animator.SetTrigger(Slice);
                attackFind = 2;
            }
        }
    }

    private void FireBallJutsu()
    {
        if (Input.GetMouseButtonDown(1) && (FirePotion.canUseFireBall == true) && fireBallCount > 0)
        {
            _animator.SetTrigger(BallJutsu);
            hitCollider.SetActive(true);
            _audioManager.PlayingFireBallSFX();
            attackFind = 1;
        }

        if (fireBallCount == 0)
        {
            FirePotion.canUseFireBall = false;
        }
    }

    bool _isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);
    }

    bool CanEndJump()
    {
        return Physics2D.OverlapBox(jumpCheck.transform.position, new Vector2(2, 2), 0f, whatIsGrounded);
    }

    public void CountingFireBall()
    {
        fireBallCount--;
    }

    public void RunningSlice0()
    {
        _canMove = false;
        _runningSlice = true;
    }
    public void RunningSlice1()
    {
        Invoke(nameof(CanMove),0.09f);
        
    }

    private void CanMove()
    {
        _canMove = true;
        _runningSlice = false;
    }
    public void HitBoxOff()
    {
       hitCollider.SetActive(false);
    }

    private void LadderClimbing()
    {
        if (!_ladderDedector.LadderDedected)
        {
            //_isClimbingLadder = false;
            _animator.SetBool(ClimbingLadder,false);
            return;
        }
        float _ladderInput = Input.GetAxis("Vertical");
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _ladderInput);

        if (_ladderInput > 0)
        {
            _isClimbingLadder = true;
            _animator.SetBool(ClimbingLadder, true);
            _animator.SetBool("LadderIdle", false);
        }
        else if (_ladderInput == 0)
        {
            _isClimbingLadder = false;
            _animator.SetBool("LadderIdle", true);
            _rigidbody.gravityScale = 0;
        }
        if(_isGrounded() && _isClimbingLadder) _animator.SetBool(ClimbingLadder, false);


    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(jumpCheck.position, new Vector2(2, 2));
    }
    private void HPslider()
    {
        warriorHpSlider.value = hpPlayer;
        if (hpPlayer < 35) _hpFillImage.sprite = _lowHpimage;
        if (hpPlayer >= 35) _hpFillImage.sprite = _highHpimage;
    }

    private void BlindingEnemies()
    {
        if(Input.GetKeyDown(KeyCode.C) && blindCount > 0)
        {
            _animator.SetTrigger(Blind);
        }
    }
    public void MakeBlindingTrue()
    {
        isBlinding = true;
    }
    public void MakeBlindingFalse()
    {
        isBlinding = false;
    }
    public void InstantiateBlindBall()
    {
        
        Instantiate(_blindBallPrefab, _blindBallPoint.position,Quaternion.identity);
    }

    private void DeathCheck()
    {
        if(hpPlayer <= 0)
        {
            isDead = true;
            _canMove = false;
            _rigidbody.velocity = Vector3.zero;
            _animator.SetTrigger(Dead);
            Invoke(nameof(DestroyChar), 1.5f);

        }
        else
        {
            isDead = false;
        }
    }
    private void DestroyChar()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    public void CountingBlind()
    {
        blindCount--;
    }
    public void PlayingSwordSFX()
    {
        _audioManager.PlayingSwordSFX();
    }

}
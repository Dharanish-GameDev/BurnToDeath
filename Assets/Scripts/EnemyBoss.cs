
using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class EnemyBoss : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    public float hitPoints;
    public float maxHitPoints;
    public EnemyHealthBar healthBar;
    public bool isDead = false;
    #endregion

    #region Private Variables
    private RaycastHit2D _hit;
    private Transform _target;
    private Animator _animator;
    private float _distance;
    private bool _attackMode;
    private bool _inRange;
    private bool _isCooling;
    private float _intTimer;
    [SerializeField] private GameObject _hltClaimPrefab;
    [SerializeField] private bool gaveHealth;
    [SerializeField] private bool isBlinded;
    [SerializeField] private GameObject _faceBlind;
    [SerializeField] private GameObject _hitBox;
    [SerializeField] private SpriteRenderer[] _sprite;
    [SerializeField] private GameObject _healthSlider;
    [SerializeField] private RedChest redChest;
    [SerializeField] private GameObject _deadParticles;
    [SerializeField] private  CinemachineVirtualCamera virtualCamera;
    private bool notShakedScreen;
    #endregion

    private void Awake()
    {
        SelectTarget();
        _intTimer = timer;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        hitPoints = maxHitPoints;
        healthBar.EnemyHeathBar(hitPoints, maxHitPoints);
    }

    void Update()
    {

        if (!_attackMode && !isDead && !isBlinded)
        {
            Move();
        }
        else
        {
            _animator.SetBool("CanWalk", false);
        }
        if (!InsideOfLimits() && !_inRange && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Boss_Attack"))
        {
            SelectTarget();
        }
        if (_inRange)
        {
            _hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RayCastDebugger();
        }

        // when player detected

        if (_hit.collider != null)
        {
            EnemyLogic();
        }

        if (_hit.collider == null)
        {
            _inRange = false;
        }

        if (_inRange == false || isDead || isBlinded)
        {
            StopAttack();
        }
        Dead();
        BlindCheck();

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _target = other.transform;
            _inRange = true;
            Flip();
        }
    }

    void RayCastDebugger()
    {
        if (_distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > _distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    void EnemyLogic()
    {
        _distance = Vector2.Distance(transform.position, _target.position);
        if (_distance > attackDistance)
        {
            StopAttack();
        }
        if (attackDistance >= _distance && !_isCooling && !_attackMode)
        {
            Attack();
        }
        if (_isCooling)
        {
            _animator.SetBool("Attack", false);
            CoolDown();
        }
    }
    void Move()
    {
        _animator.SetBool("CanWalk", true);
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Boss_Attack"))
        {
            Vector2 targetPosition = new Vector2(_target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
    void Attack()
    {
        timer = _intTimer;
        _attackMode = true;
        _animator.SetBool("CanWalk", false);
        _animator.SetBool("Attack", true);
    }
    void StopAttack()
    {
        _isCooling = false;
        _attackMode = false;
        _animator.SetBool("Attack", false);
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && _isCooling && _attackMode)
        {
            _isCooling = false;
            _attackMode = false;
        }
    }
    public void TriggerCooling()
    {
        _isCooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        if (distanceToLeft > distanceToRight)
        {
            _target = leftLimit;
        }
        else
        {
            _target = rightLimit;
        }
        Flip();

    }

    private void Flip()
    {
        if (_attackMode || isDead || isBlinded)
        {
            return;
        }

        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > _target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }

    private void Dead()
    {
        if (hitPoints <= 0)
        {
            isDead = true;
            for(int i = 0;i<_sprite.Length;i++)
            {
                _sprite[i].enabled = false;
                redChest._bossDestroyed = true;
            }
            if (_deadParticles != null)
            {
                _deadParticles.SetActive(true);
                if (_deadParticles.GetComponent<ParticleSystem>().isPlaying == false)
                {
                    _deadParticles.GetComponent<ParticleSystem>().Play();
                }
            }
            StartCoroutine(ScreenShake());
            _healthSlider.SetActive(false);
            Invoke(nameof(GivingHealth), 1.3f);
           
        }

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
                _hitBox.SetActive(false);
                if (!_faceBlind.activeInHierarchy)
                {
                    _faceBlind.SetActive(true);
                }
            }
        }
        else
        {
            isBlinded = false;
            _hitBox.SetActive(true);
            if (_faceBlind.activeInHierarchy)
            {
                _faceBlind.SetActive(false);
            }
        }
    }
    private void GivingHealth()
    {
        Vector2 tempPos = transform.position;
        Quaternion tempRot = transform.position.x < _target.transform.position.x ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        if (!gaveHealth)
        {
            Instantiate(_hltClaimPrefab, tempPos, tempRot);
            gaveHealth = true;
        }
    }
    IEnumerator ScreenShake()
    {
        if(notShakedScreen == false)
        {
            CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_TrackedObjectOffset = new Vector3(0.15f, -0.21f, 0);
                yield return new WaitForSeconds(0.3f);
                (componentBase as CinemachineFramingTransposer).m_TrackedObjectOffset = new Vector3(-0.22f, 0.12f, 0);
                yield return new WaitForSeconds(0.3f);
                (componentBase as CinemachineFramingTransposer).m_TrackedObjectOffset = new Vector3(0, 0f, 0);
                notShakedScreen = true;
            }
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2.5f;

    public BoxCollider2D DamageCollider;
    public CircleCollider2D DetectionRadius;
    public CircleCollider2D AttackRadius;

    private GameObject IncomingArrow;
    private Collider2D ArrowCollider;

    public int health = 100;
    public int SelfDamage = 40;
    private bool NoticedPlayer;

    Transform PlayerPosition;
    GameObject Player;
    Collider2D PlayerCollider;
    Animator _animator;
    PlayerMovement PlayerScript;
    AnimatorClipInfo[] EnemyClipInfo;
    private Rigidbody2D _rb;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    private const string _isAttacking = "isAttacking";
    private const string _gotHurt = "gotHurt";
    private const string _isDead = "isDead";

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        PlayerCollider = Player.GetComponent<CircleCollider2D>();
        PlayerPosition = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool gotHurt = _animator.GetBool("gotHurt");
        bool isDead = _animator.GetBool("isDead");
        if (NoticedPlayer == true && gotHurt == false && isDead == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, PlayerPosition.transform.position, _moveSpeed * Time.deltaTime);
            Vector3 VectorToPlayer = transform.position - PlayerPosition.transform.position;
            _animator.SetFloat(_horizontal, VectorToPlayer.x);
            _animator.SetFloat(_vertical, VectorToPlayer.y);
            _animator.SetFloat(_lastHorizontal, VectorToPlayer.x);
            _animator.SetFloat(_lastVertical, VectorToPlayer.y);
        }
        bool DebugIsAttacking = _animator.GetBool("isAttacking");
    }
    public void TakeDamage (int damage)
    {
        health -= damage;

        _animator.SetBool(_gotHurt, true); // Set gotHurt to false at the end of the hurt animation
        
        if (health <= 0)
        {
            _animator.SetBool(_isDead, true);
        }
    }

    public void DealDamageUp()
    {
        EnemyClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        if (EnemyClipInfo[0].clip.name == "Attack_Up")
        {
            if (AttackRadius.IsTouching(PlayerCollider))
            {
                Player.GetComponent<PlayerMovement>().TakeDamage();
            }
        }
    }
    public void DealDamageDown()
    {
        EnemyClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        if (EnemyClipInfo[0].clip.name == "Attack_Down")
        {
            if (AttackRadius.IsTouching(PlayerCollider))
            {
                Player.GetComponent<PlayerMovement>().TakeDamage();
            }
        }
    }
    public void DealDamageLeft()
    {
        EnemyClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        if (EnemyClipInfo[0].clip.name == "Attack_Left")
        {
            if (AttackRadius.IsTouching(PlayerCollider))
            {
                Player.GetComponent<PlayerMovement>().TakeDamage();
            }
        }
    }
    public void DealDamageRight()
    {
        EnemyClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        if (EnemyClipInfo[0].clip.name == "Attack_Right")
        {
            if (AttackRadius.IsTouching(PlayerCollider))
            {
                Player.GetComponent<PlayerMovement>().TakeDamage();
            }
        }
    }

    void Die() // Call this at the end of the death animation
    {
        Destroy(gameObject);
    }

    void Recover()
    {
        _animator.SetBool(_gotHurt, false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            if (DamageCollider.IsTouching(collision))
            {
                TakeDamage(SelfDamage);
                Destroy(collision.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (DetectionRadius.IsTouching(collision) && collision.gameObject.tag == "Player" && collision.GetType() == typeof(CircleCollider2D))
        {
            NoticedPlayer = true;
        }
        if (AttackRadius.IsTouching(collision) && collision.gameObject.tag == "Player" && collision.GetType() == typeof(CircleCollider2D))
        {
            _animator.SetBool(_isAttacking, true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (DetectionRadius.IsTouching(collision) == false && collision.GetType() == typeof(CircleCollider2D))
        {
            NoticedPlayer = false;
            _animator.SetFloat(_horizontal, 0);
            _animator.SetFloat(_vertical, 0);
        }
        if (AttackRadius.IsTouching(collision) == false && collision.GetType() == typeof(CircleCollider2D))
        {
            _animator.SetBool(_isAttacking, false);
        }
    }
}

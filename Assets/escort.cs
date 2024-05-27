using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class escort : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2.5f;

    public CircleCollider2D DetectionRadius;

    public bool NoticedPlayer;
    public bool hasInitiatedDialogue;

    Transform PlayerPosition;
    Animator _animator;
    private GameObject Player;
    private PlayerMovement PlayerScript;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerMovement>();
        PlayerPosition = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (NoticedPlayer == true && PlayerScript.hasEscortedAdventurer == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, PlayerPosition.transform.position, _moveSpeed * Time.deltaTime);
            Vector3 VectorToPlayer = transform.position - PlayerPosition.transform.position;
            _animator.SetFloat(_horizontal, VectorToPlayer.x);
            _animator.SetFloat(_vertical, VectorToPlayer.y);
            _animator.SetFloat(_lastHorizontal, VectorToPlayer.x);
            _animator.SetFloat(_lastVertical, VectorToPlayer.y);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (DetectionRadius.IsTouching(collision) && collision.gameObject.tag == "Player")
        {
            NoticedPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (DetectionRadius.IsTouching(collision) == false)
        {
            NoticedPlayer = false;
            _animator.SetFloat(_horizontal, 0);
            _animator.SetFloat(_vertical, 0);
        }
    }
}

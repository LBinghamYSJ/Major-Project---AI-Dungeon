using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    Animator _animator;

    public Transform ArrowSpawnPointDown;
    public Transform ArrowSpawnPointUp;
    public Transform ArrowSpawnPointLeft;
    public Transform ArrowSpawnPointRight;
    public GameObject ArrowPrefab;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    public const string _lastHorizontal = "LastHorizontal";
    public const string _lastVertical = "LastVertical";
    private const string _isAttacking2 = "IsAttacking";
    bool isAttackingDown;
    bool isAttackingUp;
    bool isAttackingLeft;
    bool isAttackingRight;
    private int health = 4;
    private GameObject RespawnPoint;
    private GameObject Player;
    private static PlayerMovement playerInstance;
    private GameObject Escort;
    private escort EscortScript;
    private GameObject NPCGuestGiver;
    private QuestGiver QuestGiverScript;
    private OpenAPIController OpenAPIScript;
    private Vector3 HumanCraftedDungeonSpawnPoint = new Vector3(-17.02375f, 2.925701f, 0f);
    private Vector3 NormalDungeonExitSpawnPoint = new Vector3(-0.5084311f, 20.06587f, 0f);
    private Vector3 AIDungeonExitSpawn = new Vector3(-12.45371f, 20.06664f, 0f);
    private Vector3 EscortExitSpawn = new Vector3(-8.1445f, 15.28071f, 0f);
    public bool hasEscortedAdventurer;
    private GameObject Exit;
    public bool hasInitiatedQuest;

    public Image[] Hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();


        foreach (Image img in Hearts)
        {
            img.sprite = EmptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            Hearts[i].sprite = FullHeart;
        }

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        OpenAPIScript = GetComponent<OpenAPIController>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InputManager._isInteracting = false;
        if (SceneManager.GetActiveScene().name == "World" && hasEscortedAdventurer == true)
        {
            Escort.transform.parent = null;
            SceneManager.MoveGameObjectToScene(Escort, SceneManager.GetActiveScene());
        }
        if (SceneManager.GetActiveScene().name == "HumanCraftedDungeon")
        {
            Player.transform.position = HumanCraftedDungeonSpawnPoint;
        }
    }

    public void GrabEscort()
    {
        if (SceneManager.GetActiveScene().name == "AIDungeon")
        {
            Escort = GameObject.FindWithTag("Escort");
            EscortScript = Escort.GetComponent<escort>();
        }
    }

    private void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        _rb.velocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }

        if (InputManager._isAttacking == true)
        {
            _animator.SetBool(_isAttacking2, true);
        }
        if (InputManager._isAttacking != true)
        {
            _animator.SetBool(_isAttacking2, false);
        }
    }

    public void TakeDamage()
    {
        if (health >= 1)
        {
            health -= 1;
            foreach (Image img in Hearts)
            {
                img.sprite = EmptyHeart;
            }
            for (int i = 0; i < health; i++)
            {
                Hearts[i].sprite = FullHeart;
            }
        }
        else
        {
            RespawnPoint = GameObject.FindWithTag("RespawnPoint");
            health = 4;
            transform.position = RespawnPoint.transform.position;
            for (int i = 0; i < health; i++)
            {
                Hearts[i].sprite = FullHeart;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (InputManager._isInteracting == true && SceneManager.GetActiveScene().name == "AIDungeon" && EscortScript.NoticedPlayer == true && collision.gameObject.tag == "Exit")
        {
            Player.transform.position = AIDungeonExitSpawn;
            Escort.transform.position = EscortExitSpawn;
            InputManager._isInteracting = false;
            hasEscortedAdventurer = true;
            SceneManager.LoadScene("World");
        }
        if (InputManager._isInteracting == true && SceneManager.GetActiveScene().name == "HumanCraftedDungeon" && collision.gameObject.tag == "Exit")
        {
            Player.transform.position = NormalDungeonExitSpawnPoint;
            InputManager._isInteracting = false;
            SceneManager.LoadScene("World");
        }
        if (SceneManager.GetActiveScene().name == "AIDungeon" && EscortScript.hasInitiatedDialogue == false && collision.gameObject.tag == "Escort")
        {
            OpenAPIScript.GenerateDialogue();
            EscortScript.hasInitiatedDialogue = true;
        }
        if (SceneManager.GetActiveScene().name == "AIDungeon")
        {
            if (EscortScript.hasInitiatedDialogue == false && collision.gameObject.tag == "Escort")
            {
                OpenAPIScript.GenerateDialogue();
                EscortScript.hasInitiatedDialogue = true;
            }
        }
        if (SceneManager.GetActiveScene().name == "World")
        {
            NPCGuestGiver = GameObject.FindWithTag("QuestGiver");
            QuestGiverScript = NPCGuestGiver.GetComponent<QuestGiver>();
            if (InputManager._isInteracting == true && hasInitiatedQuest == true && hasEscortedAdventurer == false && collision.gameObject.tag == "AIDungeonEntrance")
            {
                InputManager._isInteracting = false;
                SceneManager.LoadScene("AIDungeon");
            }
            if (InputManager._isInteracting == true && collision.gameObject.tag == "NormalDungeonEntrance")
            {
                SceneManager.LoadScene("HumanCraftedDungeon");
            }
            if (InputManager._isInteracting == true && collision.gameObject.tag == "QuestGiver")
            {
                if (hasInitiatedQuest == false)
                {
                    QuestGiverScript.GenerateDialogue();
                    hasInitiatedQuest = true;
                }
                else
                {
                    QuestGiverScript.GenerateDialogue();
                }
            }
        }
    }
    public void InstantiateDown()
    {
        Instantiate(ArrowPrefab, ArrowSpawnPointDown.position, ArrowSpawnPointDown.rotation);
    }
    public void InstantiateUp()
    {
        Instantiate(ArrowPrefab, ArrowSpawnPointUp.position, ArrowSpawnPointUp.rotation);
    }
    public void InstantiateLeft()
    {
        Instantiate(ArrowPrefab, ArrowSpawnPointLeft.position, ArrowSpawnPointLeft.rotation);
    }
    public void InstantiateRight()
    {
        Instantiate(ArrowPrefab, ArrowSpawnPointRight.position, ArrowSpawnPointRight.rotation);
    }
}

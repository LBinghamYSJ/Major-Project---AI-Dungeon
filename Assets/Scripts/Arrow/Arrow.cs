using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector2 _movement;

    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject Player;
    private Animator PlayerAnimator;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerAnimator = Player.GetComponent<Animator>();

        if (PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack_Down")
        {
            rb.velocity = -transform.up * speed; // Fire downwards
            Destroy(gameObject, 5);
        }
        if (PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack_Up")
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            rb.velocity = -transform.up * speed; // Fire upwards
            Destroy(gameObject, 5);
        }
        if (PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack_Left")
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            rb.velocity = -transform.up * speed; // Fire leftwards
            Destroy(gameObject, 5);
        }
        if (PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack_Right")
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            rb.velocity = -transform.up * speed; // Fire rightwards
            Destroy(gameObject, 5);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Escort")
        {
            Destroy(gameObject);
        }
    }
}

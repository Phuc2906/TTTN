 using UnityEngine;

public class TeammateMove : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float moveSpeed = 2f;
    public float stopDistance = 1.5f;

    private EnemyAttack attack;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;      

    void Start()
    {
        attack = GetComponent<EnemyAttack>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();      

        if (!player)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    void Update()
    {
        if ((attack != null && attack.isAttacking) || player == null)
        {
            SetRunning(false);   
            return;
        }

        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;

        if (distance > stopDistance)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

            SetRunning(true);    

            if (sr != null)
                sr.flipX = direction.x < 0;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            SetRunning(false);   
        }
    }

    void SetRunning(bool state)
    {
        if (anim != null)
            anim.SetBool("IsRunning", state);   
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    public float attackCooldown = 0.6f;

    [HideInInspector] public bool isAttacking;

    private float lastHitTime;
    private Rigidbody2D rb;
    private Collision2D currentCollision;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsTarget(collision)) return;

        currentCollision = collision;
        isAttacking = true;

        TryAttack();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != currentCollision) return;

        TryAttack();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == currentCollision)
        {
            currentCollision = null;
            isAttacking = false;
        }
    }

    void TryAttack()
    {
        if (currentCollision == null) return;

        rb.WakeUp(); 

        if (Time.time - lastHitTime < attackCooldown) return;

        DealDamage(currentCollision);
        lastHitTime = Time.time;
    }

    bool IsTarget(Collision2D col)
    {
        return col.collider.CompareTag("Player")
            || col.collider.GetComponent<TeammateHealth>()
            || col.collider.GetComponent<HealthRuby>()
            || col.collider.GetComponent<HealthWall>();
    }

    void DealDamage(Collision2D collision)
    {
        Collider2D c = collision.collider;

        if (c.TryGetComponent(out PlayerHealth p))
            p.TakeDamage(damage);
        else if (c.TryGetComponent(out TeammateHealth t))
            t.TakeDamage(damage);
        else if (c.TryGetComponent(out HealthRuby r))
            r.TakeDamage(damage);
        else if (c.TryGetComponent(out HealthWall w))
            w.TakeDamage(damage);
    }
}

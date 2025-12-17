using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    public float attackCooldown = 0.6f;

    [HideInInspector] public bool isAttacking;

    private float lastHitTime;
    private Rigidbody2D rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryAttack(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryAttack(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsTarget(collision))
            isAttacking = false;
    }

    void TryAttack(Collision2D collision)
    {
        if (!IsTarget(collision)) return;

        isAttacking = true;

        if (rb)
            rb.velocity = Vector2.zero;

        if (Time.time - lastHitTime < attackCooldown) return;

        DealDamage(collision);
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
        if (collision.collider.TryGetComponent(out PlayerHealth p))
            p.TakeDamage(damage);
        else if (collision.collider.TryGetComponent(out TeammateHealth t))
            t.TakeDamage(damage);
        else if (collision.collider.TryGetComponent(out HealthRuby r))
            r.TakeDamage(damage);
        else if (collision.collider.TryGetComponent(out HealthWall w))
            w.TakeDamage(damage);
    }
}

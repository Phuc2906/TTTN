using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;
    public float attackCooldown = 0.5f;

    [HideInInspector] public bool isAttacking = false;

    private float lastHitTime;
    private EnemyMove moveScript;
    private Rigidbody2D rb;

    private void Start()
    {
        moveScript = GetComponent<EnemyMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        isAttacking = true;

        if (moveScript != null) moveScript.enabled = false;
        if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;

        DealDamage(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        isAttacking = true;

        if (Time.time - lastHitTime < attackCooldown) return;

        DealDamage(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        isAttacking = false;

        if (moveScript != null) moveScript.enabled = true;
        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void DealDamage(Collision2D collision)
{
    PlayerHealth playerHp = collision.collider.GetComponent<PlayerHealth>();
    if (playerHp != null)
    {
        playerHp.TakeDamage(damage);
        lastHitTime = Time.time;
        return; 
    }

    HealthRuby rubyHp = collision.collider.GetComponent<HealthRuby>();
    if (rubyHp != null)
    {
        rubyHp.TakeDamage(damage);
        lastHitTime = Time.time;
        return; 
    }

    HealthWall wallHp = collision.collider.GetComponent<HealthWall>();
    if (wallHp != null)
    {
        wallHp.TakeDamage(damage);
        lastHitTime = Time.time;
    }
}
}

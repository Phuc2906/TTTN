using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int normalDamage = 1;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("EnemyBoss")) return;

        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy == null) return;

        int finalDamage = DamageManager.instance.GetDamage(normalDamage);

        enemy.TakeDamage(finalDamage);
        Destroy(gameObject);
    }
}
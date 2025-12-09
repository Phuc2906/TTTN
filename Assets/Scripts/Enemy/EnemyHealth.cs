using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public EnemySpawner spawner;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

   void Die()
{
    isDead = true;
    anim.SetTrigger("Dead");

    FindObjectOfType<PlayerExpManager>().GainExp(5); 

    if (spawner != null)
        spawner.OnEnemyKilled();

    Collider2D col = GetComponent<Collider2D>();
    if (col != null) col.enabled = false;

    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.isKinematic = true;

    // Tắt hết script khác trừ script này
    foreach (var script in GetComponents<MonoBehaviour>())
    {
        if (script != this)
            script.enabled = false;
    }

    // Destroy súng con (nếu có)
    EnemyGun gun = GetComponentInChildren<EnemyGun>();
    if (gun != null)
        Destroy(gun.gameObject);

    // Xóa hết đạn địch trên màn hình
    foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet_Enemy"))
    {
        Destroy(bullet);
    }

    // QUAN TRỌNG: ĐM CÓ DẤU NGOẶC ĐÓNG ĐÂY NÈ!
    Destroy(gameObject, 1f);
}
}
}

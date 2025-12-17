using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 10;
    public int maxExp = 5;
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;

    public EnemySpawner spawner;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Dead");

        FindObjectOfType<PlayerExpManager>().GainExp(maxExp);

        if (spawner != null)
            spawner.OnEnemyKilled();

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;

        foreach (var script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = false;
        }

        EnemyGun gun = GetComponentInChildren<EnemyGun>();
        if (gun != null)
            Destroy(gun.gameObject);

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet_Enemy"))
        {
            Destroy(bullet);
        }

        Destroy(gameObject, 1f);
    }
}

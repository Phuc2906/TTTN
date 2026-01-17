using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int maxExp = 5;
    private int currentHealth;

    public Slider healthBar;

    private Animator anim;
    private bool isDead = false;

    private EnemySave save;

    void Start()
    {
        anim = GetComponent<Animator>();
        save = GetComponent<EnemySave>();

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

        if (save != null)
            save.Collect();
        else
            Destroy(gameObject, 1f);
    }
}

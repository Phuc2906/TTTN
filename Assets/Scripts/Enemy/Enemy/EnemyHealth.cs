using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy ID")]
    public int enemyID = 0;

    public int maxHealth = 10;
    public int maxExp = 5;
    private int currentHealth;

    public Slider healthBar;
    public TMP_Text healthValueText;

    private Animator anim;
    private bool isDead = false;

    private EnemySave save;

    private string HEALTH_KEY;

    void Start()
    {
        anim = GetComponent<Animator>();
        save = GetComponent<EnemySave>();

        HEALTH_KEY = "EnemyHealth_" + enemyID;

        if (PlayerPrefs.HasKey(HEALTH_KEY))
            currentHealth = PlayerPrefs.GetInt(HEALTH_KEY);
        else
            currentHealth = maxHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        UpdateHealthText();

        PlayerPrefs.SetInt(HEALTH_KEY, currentHealth);
        PlayerPrefs.Save();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (healthValueText != null)
        {
            healthValueText.text = $"{currentHealth}/{maxHealth}";
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

        currentHealth = 0;
        UpdateHealthText();

        PlayerPrefs.SetInt(HEALTH_KEY, 0);
        PlayerPrefs.Save();

        if (save != null)
            save.Collect();
        else
            Destroy(gameObject, 1f);
    }
}

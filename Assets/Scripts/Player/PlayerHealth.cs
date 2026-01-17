using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Máu player")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public HealthBar healthBar;
    public GameObject gameOverCanvas;
    public GameObject player;

    [Header("PlayerPrefs key")]
    public string playerRefKey = "PlayerHealth";

    void Awake()
    {
        if (PlayerPrefs.HasKey(playerRefKey))
            currentHealth = PlayerPrefs.GetInt(playerRefKey);
        else
            currentHealth = maxHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - dmg, 0);

        UpdateUI();
        SaveHealth();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;
        if (currentHealth >= maxHealth) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        UpdateUI();
        SaveHealth();
    }

    void UpdateUI()
    {
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            Destroy(bullet);

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (player != null)
            player.SetActive(false);

        Time.timeScale = 0f;
    }

    void SaveHealth()
    {
        PlayerPrefs.SetInt(playerRefKey, currentHealth);
        PlayerPrefs.Save();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public bool IsHealthFull()
    {
        return currentHealth >= maxHealth;
    }
}

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

    void Start()
    {
        if (PlayerPrefs.HasKey(playerRefKey))
            currentHealth = PlayerPrefs.GetInt(playerRefKey);
        else
            currentHealth = maxHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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
        currentHealth = Mathf.Max(currentHealth - dmg, 0);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        SaveHealth();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        SaveHealth();
    }

    void Die()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            Destroy(bullet);

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        player.SetActive(false);
        Time.timeScale = 0f;
    }

    void SaveHealth()
    {
        PlayerPrefs.SetInt(playerRefKey, currentHealth);
        PlayerPrefs.Save();
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}

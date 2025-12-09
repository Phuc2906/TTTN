using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public HealthBar healthBar;
    public GameObject gameOverCanvas;
    public GameObject player;
    public GameObject bullet;

    void Start()
    {
    if (PlayerPrefs.HasKey("PlayerHealth"))
        currentHealth = PlayerPrefs.GetInt("PlayerHealth");  
    else
        currentHealth = maxHealth;  

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
        currentHealth -= dmg;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        if (healthBar != null) healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        player.SetActive(false);

        Time.timeScale = 0f;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void SetHealth(int value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        if (healthBar != null) healthBar.SetHealth(currentHealth);
    }


}

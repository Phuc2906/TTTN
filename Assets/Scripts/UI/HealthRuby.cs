using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthRuby : MonoBehaviour
{
    [Header("Máu tối đa")]
    public int maxHealth = 500;

    [Header("Thanh máu UI")]
    public Slider healthBar;
    public TMP_Text healthValueText;

    [Header("Canvas GameOver")]
    public GameObject gameOverCanvas;

    [Header("Game Over")]
    public bool pauseGameOnDeath = true;

    private int currentHealth;

    private const string HEALTH_RUBY_KEY = "HealthRuby";

    private void Start()
    {
        if (PlayerPrefs.HasKey(HEALTH_RUBY_KEY))
            currentHealth = PlayerPrefs.GetInt(HEALTH_RUBY_KEY);
        else
            currentHealth = maxHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("Chưa gán HealthBar vào HealthRuby!");
        }

        UpdateHealthText();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        SaveHealth();
        UpdateHealthBar();
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        SaveHealth();
        UpdateHealthBar();
        UpdateHealthText();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    private void UpdateHealthText()
    {
        if (healthValueText != null)
        {
            healthValueText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    private void Die()
    {
        currentHealth = 0;
        SaveHealth();
        UpdateHealthBar();
        UpdateHealthText();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (pauseGameOnDeath)
            Time.timeScale = 0f;

        gameObject.SetActive(false);
    }

    private void SaveHealth()
    {
        PlayerPrefs.SetInt(HEALTH_RUBY_KEY, currentHealth);
        PlayerPrefs.Save();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

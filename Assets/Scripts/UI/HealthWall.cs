using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthWall : MonoBehaviour
{
    [Header("Máu tối đa")]
    public int maxHealth = 500;

    [Header("Thanh máu UI")]
    public Slider healthBar;
    public TMP_Text healthValueText;

    private int currentHealth;

    private const string HEALTH_WALL_KEY = "HealthWall";
    private const string HEALTH_WALL_DEAD_KEY = "HealthWall_Dead";

    private void Start()
    {
        if (PlayerPrefs.GetInt(HEALTH_WALL_DEAD_KEY, 0) == 1)
        {
            Destroy(gameObject);
            return;
        }

        if (PlayerPrefs.HasKey(HEALTH_WALL_KEY))
            currentHealth = PlayerPrefs.GetInt(HEALTH_WALL_KEY);
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
            Debug.LogWarning("Chưa gán HealthBar vào HealthWall!");
        }

        UpdateHealthText();
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

        PlayerPrefs.SetInt(HEALTH_WALL_DEAD_KEY, 1);
        PlayerPrefs.SetInt(HEALTH_WALL_KEY, 0);
        PlayerPrefs.Save();

        UpdateHealthBar();
        UpdateHealthText();

        Destroy(gameObject);
    }

    private void SaveHealth()
    {
        PlayerPrefs.SetInt(HEALTH_WALL_KEY, currentHealth);
        PlayerPrefs.Save();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

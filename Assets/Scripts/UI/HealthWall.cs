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

    [Header("Immortal")]
    public GameObject immortal;

    [Header("Input")]
    public TMP_InputField healthInputField;
    public Canvas healthCanvas;

    [Header("Text Debug / ToString")]
    public TMP_Text healthText;

    private int currentHealth;

    private const string HEALTH_WALL_KEY = "HealthWall";
    private const string HEALTH_WALL_MAX_KEY = "HealthWallMax";
    private const string HEALTH_WALL_DEAD_KEY = "HealthWall_Dead";

    private void Start()
    {
        if (PlayerPrefs.GetInt(HEALTH_WALL_DEAD_KEY, 0) == 1)
        {
            Destroy(gameObject);
            return;
        }

        // load max
        maxHealth = PlayerPrefs.GetInt(HEALTH_WALL_MAX_KEY, maxHealth);

        // load current
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

        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        if (immortal != null && immortal.activeSelf)
            return;

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

    // ======================
    // INPUT SET HEALTH (GIỮ LOGIC GAME)
    // ======================
    public void SetHealthWall()
    {
        if (healthInputField == null) return;

        if (int.TryParse(healthInputField.text, out int value))
        {
            currentHealth = value;
            maxHealth = value;

            SaveHealth();
            SaveMaxHealth();

            UpdateHealthBar();
            UpdateHealthText();

            if (healthCanvas != null)
                healthCanvas.gameObject.SetActive(false);
        }
    }

    // ======================
    // UI
    // ======================
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    private void UpdateHealthText()
    {
        if (healthValueText != null)
        {
            healthValueText.text = $"{currentHealth}/{maxHealth}";
        }

        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    // ======================
    // SAVE
    // ======================
    private void SaveHealth()
    {
        PlayerPrefs.SetInt(HEALTH_WALL_KEY, currentHealth);
        PlayerPrefs.Save();
    }

    private void SaveMaxHealth()
    {
        PlayerPrefs.SetInt(HEALTH_WALL_MAX_KEY, maxHealth);
        PlayerPrefs.Save();
    }

    // ======================
    // DEATH (GIỮ NGUYÊN LOGIC)
    // ======================
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

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
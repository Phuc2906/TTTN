using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthRuby : MonoBehaviour
{
    [Header("Máu")]
    public int maxHealth = 500;

    [Header("UI")]
    public Slider healthBar;
    public TMP_Text healthValueText;
    public TMP_Text healthText;

    [Header("Game Over")]
    public GameObject gameOverCanvas;
    public bool pauseGameOnDeath = true;

    [Header("Immortal")]
    public GameObject immortal;

    [Header("Input")]
    public TMP_InputField healthInputField;
    public Canvas healthCanvas;

    private int currentHealth;

    private const string HEALTH_KEY = "HealthRuby";
    private const string HEALTH_MAX_KEY = "HealthRubyMax";

    private void Start()
    {
        // Load max health
        maxHealth = PlayerPrefs.GetInt(HEALTH_MAX_KEY, maxHealth);

        // Load current health
        currentHealth = PlayerPrefs.GetInt(HEALTH_KEY, maxHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateUI();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (immortal != null && immortal.activeSelf) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        SaveData();
        UpdateUI();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        SaveData();
        UpdateUI();
    }

    // ======================
    // INPUT SET HEALTH
    // ======================
    public void SetHealthRuby()
    {
        if (healthInputField == null) return;

        if (int.TryParse(healthInputField.text, out int value))
        {
            // set cả max + current luôn
            maxHealth = value;
            currentHealth = value;

            SaveData();

            UpdateUI();

            if (healthCanvas != null)
                healthCanvas.gameObject.SetActive(false);
        }
    }

    // ======================
    // UI UPDATE
    // ======================
    private void UpdateUI()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (healthValueText != null)
            healthValueText.text = $"{currentHealth}/{maxHealth}";

        if (healthText != null)
            healthText.text = currentHealth.ToString();
    }

    // ======================
    // SAVE
    // ======================
    private void SaveData()
    {
        PlayerPrefs.SetInt(HEALTH_KEY, currentHealth);
        PlayerPrefs.SetInt(HEALTH_MAX_KEY, maxHealth);
        PlayerPrefs.Save();
    }

    // ======================
    // DEATH
    // ======================
    private void Die()
    {
        currentHealth = 0;
        SaveData();
        UpdateUI();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (pauseGameOnDeath)
            Time.timeScale = 0f;

        gameObject.SetActive(false);
    }

    public int GetCurrentHealth() => currentHealth;
}
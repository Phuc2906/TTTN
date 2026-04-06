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

    public GameObject buffCanvas; 

    [Header("Defense")]
    public GameObject shieldCanvas;

    [Header("PlayerPrefs key")]
    public string playerRefKey = "PlayerHealth";
    public string playerDeadKey = "Player_Dead"; 

    private int baseMaxHealth; 

    void Awake()
    {
        baseMaxHealth = maxHealth; 

        if (PlayerPrefs.GetInt(playerDeadKey, 0) == 1)
        {
            if (gameOverCanvas != null)
                gameOverCanvas.SetActive(true);

            Destroy(gameObject);
            return;
        }

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

    void Update()
    {
        UpdateBuffHealth(); 
    }

    void UpdateBuffHealth()
    {
        int targetMax = baseMaxHealth;

        if (buffCanvas != null && buffCanvas.activeSelf)
            targetMax += 200;

        if (maxHealth != targetMax)
        {
            maxHealth = targetMax;

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (healthBar != null)
            {
                healthBar.SetMaxHealth(maxHealth);
                healthBar.SetHealth(currentHealth);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        if (shieldCanvas != null && shieldCanvas.activeSelf)
            return;

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
        PlayerPrefs.SetInt(playerDeadKey, 1);
        PlayerPrefs.Save();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Destroy(gameObject);
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
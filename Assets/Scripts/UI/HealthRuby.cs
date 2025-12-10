using UnityEngine;
using UnityEngine.UI;

public class HealthRuby : MonoBehaviour
{
    [Header("Máu tối đa")]
    public int maxHealth = 500;

    [Header("Thanh máu UI")]
    public Slider healthBar;

    [Header("Canvas GameOver")]
    public GameObject gameOverCanvas;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("Chưa gán HealthBar vào PlayerHealth!");
        }

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} đã chết!");
        
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        gameObject.SetActive(false);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

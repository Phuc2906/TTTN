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
            Debug.LogWarning("Chưa gán HealthBar vào HealthWall!");
        }

        UpdateHealthText(); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
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
        UpdateHealthBar();
        UpdateHealthText();

        Destroy(gameObject);   
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

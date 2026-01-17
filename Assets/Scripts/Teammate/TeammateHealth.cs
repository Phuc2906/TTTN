using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeammateHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 500;
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;

    public TMP_Text healthValueText;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        UpdateHealthText(); 
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        UpdateHealthText(); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        UpdateHealthText(); 
    }

    void UpdateHealthText()
    {
        if (healthValueText != null)
        {
            healthValueText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    void Die()
    {
        isDead = true;
        currentHealth = 0;
        UpdateHealthText();
    }
}

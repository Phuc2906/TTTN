using UnityEngine;
using UnityEngine.UI;

public class HealthWall : MonoBehaviour
{
    [Header("Máu tối đa")]
    public int maxHealth = 500;

    [Header("Thanh máu UI")]
    public Slider healthBar;

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
        Destroy(gameObject);   // Khi tường bị phá thì tự hủy
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

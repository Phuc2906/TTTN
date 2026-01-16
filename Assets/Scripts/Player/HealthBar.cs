using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI healthText;

    private int maxHealth;

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;

        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        UpdateText(maxHealth);
    }

    public void SetHealth(int currentHealth)
    {
        slider.value = currentHealth;
        UpdateText(currentHealth);
    }

    void UpdateText(int currentHealth)
    {
        if (healthText != null)
            healthText.text = currentHealth + " / " + maxHealth;
    }
}

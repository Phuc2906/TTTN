using UnityEngine;
using UnityEngine.UI; 

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;

    [Header("Admin Override Damage")]
    public int adminDamage = 0;

    [Header("PlayerPrefs Key")] 
    public string damageKey = "ADMIN_DAMAGE";

    [Header("Buff Canvas (x2 damage)")]
    public GameObject damageBuffCanvas;

    [Header("UI")]
    public TMPro.TextMeshProUGUI damageText;

    [Header("Button")]
    public Button damageButton; 

    void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey(damageKey))
        {
            adminDamage = PlayerPrefs.GetInt(damageKey);
        }
    }

    void Start()
    {
        RefreshAllUI();
    }

    public void SetDamage(int value)
    {
        adminDamage = value;
        PlayerPrefs.SetInt(damageKey, adminDamage);
        PlayerPrefs.Save();
        RefreshAllUI();
    }

    public int GetDamage(int bulletDamage)
    {
        int baseDamage = (adminDamage > 0) ? adminDamage : bulletDamage;

        if (damageBuffCanvas != null && damageBuffCanvas.activeSelf)
            return baseDamage * 2;

        return baseDamage;
    }

    public void SetWeaponState(bool hasWeapon, int bulletDamage)
    {
        if (damageText == null) return;

        if (!hasWeapon)
        {
            damageText.text = "Không có vũ khí";

            if (damageButton != null)
            {
                damageButton.interactable = false;

                ColorBlock cb = damageButton.colors;
                cb.disabledColor = new Color(1f, 1f, 1f, 1f);
                damageButton.colors = cb;
            }

            return;
        }

        if (damageButton != null)
            damageButton.interactable = true;

        UpdateDamageUI(bulletDamage);
    }

    public void UpdateDamageUI(int bulletDamage)
    {
        if (damageText == null) return;

        int baseDamage = (adminDamage > 0) ? adminDamage : bulletDamage;

        bool buff = (damageBuffCanvas != null && damageBuffCanvas.activeSelf);

        int finalDamage = buff ? baseDamage * 2 : baseDamage;

        damageText.text = finalDamage.ToString();
    }

    public void RefreshAllUI()
    {
        Gun gun = FindFirstObjectByType<Gun>();

        if (gun != null && gun.bulletPrefab != null)
        {
            Bullet b = gun.bulletPrefab.GetComponent<Bullet>();
            int realDamage = (b != null) ? b.normalDamage : 0;

            SetWeaponState(true, realDamage);
        }
        else
        {
            SetWeaponState(false, 0);
        }
    }
}
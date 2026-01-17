using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExpManager : MonoBehaviour
{
    public Slider expBar;
    public TMP_Text levelText;

    [Header("0/0")]
    public TMP_Text expValueText;   

    private int level = 1;
    private int currentExp = 0;
    private int expToNextLevel;

    public static PlayerExpManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        LoadLevel();
        UpdateUI();
    }

    private int CalculateExpToNextLevel()
    {
        return 100 + (level - 1) * 50;
    }

    public void GainExp(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            level++;
            expToNextLevel = CalculateExpToNextLevel();
        }

        UpdateUI();
        SaveLevel();
    }

    void UpdateUI()
    {
        expToNextLevel = CalculateExpToNextLevel();

        expBar.maxValue = expToNextLevel;
        expBar.value = currentExp;

        levelText.text = "LV " + level;
        expValueText.text = currentExp + " / " + expToNextLevel;
    }

    void SaveLevel()
    {
        PlayerPrefs.SetInt("SavedLevel", level);
        PlayerPrefs.SetInt("SavedExp", currentExp);
        PlayerPrefs.Save();
    }

    void LoadLevel()
    {
        level = PlayerPrefs.GetInt("SavedLevel", 1);
        currentExp = PlayerPrefs.GetInt("SavedExp", 0);
        expToNextLevel = CalculateExpToNextLevel();
    }

    public int GetLevel()
    {
        return level;
    }
}

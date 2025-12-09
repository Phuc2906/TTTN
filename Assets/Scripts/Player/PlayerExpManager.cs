using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExpManager : MonoBehaviour
{
    public Slider expBar;
    public TMP_Text levelText;

    private int level = 1;
    private int currentExp = 0;
    private int expToNextLevel = 100;

    public static PlayerExpManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadLevel();
        UpdateExpBar();  
        levelText.text = "LV " + level;
    }

    private int CalculateExpToNextLevel()
    {
        return 100 * level;  
    }

    public void GainExp(int amount)
    {
        currentExp += amount;
        expToNextLevel = CalculateExpToNextLevel();

        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            level++;
            expToNextLevel = CalculateExpToNextLevel();  
        }

        UpdateExpBar();
        levelText.text = "LV " + level;
        SaveLevel();
    }

    private void UpdateExpBar()
    {
        expBar.maxValue = expToNextLevel;
        expBar.value = currentExp;
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
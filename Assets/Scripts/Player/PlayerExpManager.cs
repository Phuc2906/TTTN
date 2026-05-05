using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExpManager : MonoBehaviour
{
    public Slider expBar;
    public TMP_Text levelText;

    [Header("0/0")]
    public TMP_Text expValueText;   

    [Header("Level Value Text")]
    public TMP_Text levelValueText;

    private int level = 1;
    private int currentExp = 0;
    private int expToNextLevel;

    public static PlayerExpManager Instance;

    [Header("PlayerPrefs Keys")] 
    public string levelKey = "SavedLevel";
    public string expKey = "SavedExp";

    void Awake()
    {   
        Instance = this;
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

    public void SetLevel(int value)
    {
        if (value < 1) value = 1;

        level = value;
        currentExp = 0;

        expToNextLevel = CalculateExpToNextLevel();

        UpdateUI();
        SaveLevel();
    }

    void UpdateUI()
    {
        expToNextLevel = CalculateExpToNextLevel();

        expBar.maxValue = expToNextLevel;
        expBar.value = currentExp;

        levelText.text = "LV " + level;
        levelValueText.text = level.ToString();
        
        expValueText.text = currentExp + " / " + expToNextLevel;
    }

    void SaveLevel()
    {
        PlayerPrefs.SetInt(levelKey, level);     
        PlayerPrefs.SetInt(expKey, currentExp);  
        PlayerPrefs.Save();
    }

    void LoadLevel()
    {
        level = PlayerPrefs.GetInt(levelKey, 1);     
        currentExp = PlayerPrefs.GetInt(expKey, 0);  
        expToNextLevel = CalculateExpToNextLevel();
    }

    public int GetLevel() => level;
}
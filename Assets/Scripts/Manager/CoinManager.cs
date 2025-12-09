using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("UI hiển thị coin trong game")]
    public TextMeshProUGUI coinText_Game;      
    public TextMeshProUGUI coinText_GameWin;   
    public TextMeshProUGUI coinText_GameOver;  

    [Header("UI tổng coin (nếu có)")]
    public TextMeshProUGUI coinText_Total;

    private int coin = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            // PlayerPrefs.DeleteAll();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        coin = PlayerPrefs.GetInt("TotalCoin", 30);

        UpdateAllTexts();
    }
    public void AddCoin(int value)
    {
        coin += value;

        PlayerPrefs.SetInt("TotalCoin", coin);
        PlayerPrefs.Save();

        UpdateAllTexts();
    }
    public bool SpendCoin(int cost)
    {
        if (coin >= cost)
        {
            coin -= cost;

            PlayerPrefs.SetInt("TotalCoin", coin);
            PlayerPrefs.Save();

            UpdateAllTexts();
            return true;
        }
        return false;
    }

    private void UpdateAllTexts()
    {
        string s = coin.ToString();

        if (coinText_Game != null) coinText_Game.text = s;
        if (coinText_GameWin != null) coinText_GameWin.text = s;
        if (coinText_GameOver != null) coinText_GameOver.text = s;
        if (coinText_Total != null) coinText_Total.text = s;
    }

    public int GetCoin() => coin;
}

using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private const string COIN_KEY = "Coin";

    [Header("UI")]
    public TextMeshProUGUI coinText_Game;
    public TextMeshProUGUI coinText_GameWin;
    public TextMeshProUGUI coinText_GameOver;

    [Header("Total UI")]
    public TextMeshProUGUI coinText_Total;

    private int coin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        coin = PlayerPrefs.GetInt(COIN_KEY, 30);

        UpdateAllTexts();
    }

    public void AddCoin(int value)
    {
        coin += value;
        SaveCoin();
    }

    public bool SpendCoin(int cost)
    {
        if (coin < cost) return false;

        coin -= cost;
        SaveCoin();
        return true;
    }

    private void SaveCoin()
    {
        PlayerPrefs.SetInt(COIN_KEY, coin);
        PlayerPrefs.Save();
        UpdateAllTexts();
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

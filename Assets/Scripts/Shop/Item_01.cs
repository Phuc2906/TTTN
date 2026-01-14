using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_01 : MonoBehaviour
{
    public Button buyButton;
    public Image buttonImage;
    public TMP_Text buttonText;

    public Canvas noticeCanvas;
    public int price = 20;
    public Color normalColor = Color.yellow;
    public Color boughtColor = Color.gray;

    private bool isBought = false;
    private string playerPrefKey = "Item_01_Bought";

    private void Start()
    {
        
        isBought = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        UpdateButtonUI();
    }

    private void OnBuyButtonClicked()
    {
        
        if (isBought)
        {
            return;
        }
        
        if (CoinManager.Instance.SpendCoin(price))
        {
            isBought = true;
            
            PlayerPrefs.SetInt(playerPrefKey, 1);
            PlayerPrefs.Save();
            
            int verify = PlayerPrefs.GetInt(playerPrefKey, -1);
            UpdateButtonUI();
        }
        else
        {
            if (noticeCanvas != null)
                noticeCanvas.gameObject.SetActive(true);
        }
    }

    private void UpdateButtonUI()
    {
        if (isBought)
        {
            buttonImage.color = boughtColor;
            buttonText.text = "Đã mua";
            buyButton.interactable = false;
        }
        else
        {
            buttonImage.color = normalColor;
            buttonText.text = price.ToString();
            buyButton.interactable = true;
        }
    }
}
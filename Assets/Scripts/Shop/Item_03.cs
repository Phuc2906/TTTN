using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_03 : MonoBehaviour
{
    [Header("UI Reference")]
    public Button buyButton;
    public Image buttonImage;
    public TMP_Text buttonText;

    [Header("Notice")]
    public Canvas noticeCanvas;

    [Header("Item Properties")]
    public int price = 1000;
    public Color normalColor = Color.yellow;
    public Color boughtColor = Color.gray;

    private bool isBought = false;
    private string playerPrefKey = "Item_03_Bought";

    private void Start()
    {
        isBought = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;

        buyButton.onClick.AddListener(OnBuyButtonClicked);
        UpdateButtonUI();
    }


    private void OnBuyButtonClicked()
    {
        if (isBought) return;

        if (CoinManager.Instance == null)
        {
            return;
        }

        if (CoinManager.Instance.SpendCoin(price))
        {
            isBought = true;

            PlayerPrefs.SetInt(playerPrefKey, 1);
            PlayerPrefs.Save();
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

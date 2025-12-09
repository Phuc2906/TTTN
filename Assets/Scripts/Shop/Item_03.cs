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
    public int price = 3;
    public Color normalColor = Color.yellow;
    public Color boughtColor = Color.gray;

    private bool isBought = false;

    private void Start()
    {
        // Check UI để tránh lỗi null
        if (buyButton == null) Debug.LogError("[Item_03] buyButton chưa gán!");
        if (buttonImage == null) Debug.LogError("[Item_03] buttonImage chưa gán!");
        if (buttonText == null) Debug.LogError("[Item_03] buttonText chưa gán!");

        buyButton.onClick.AddListener(OnBuyButtonClicked);

        UpdateButtonUI();
    }

    private void OnBuyButtonClicked()
    {
        if (isBought) return;

        // Check CoinManager tồn tại
        if (CoinManager.Instance == null)
        {
            Debug.LogError("[Item_03] CoinManager.Instance = NULL! Không thể trừ coin.");
            return;
        }

        if (CoinManager.Instance.SpendCoin(price))
        {
            isBought = true;
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
            buttonText.text = "Bought";
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

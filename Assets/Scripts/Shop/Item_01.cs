using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_01 : MonoBehaviour
{
    [Header("UI Reference")]
    public Button buyButton;
    public Image buttonImage;
    public TMP_Text buttonText;

    [Header("Notice")]
    public Canvas noticeCanvas;

    [Header("Item Properties")]
    public int price = 1;
    public Color normalColor = Color.yellow;
    public Color boughtColor = Color.gray;

    private bool isBought = false;

    private void Start()
    {
        Debug.Log("[Item_01] Start() được gọi");

        // Check gán UI
        if (buyButton == null) Debug.LogError("[Item_01] buyButton chưa gán!");
        if (buttonImage == null) Debug.LogError("[Item_01] buttonImage chưa gán!");
        if (buttonText == null) Debug.LogError("[Item_01] buttonText chưa gán!");

        buyButton.onClick.AddListener(OnBuyButtonClicked);

        UpdateButtonUI();
    }

    private void OnBuyButtonClicked()
    {
        Debug.Log("[Item_01] Button được click!");

        if (isBought)
        {
            Debug.LogWarning("[Item_01] Đã mua rồi, không thể mua lại!");
            return;
        }

        if (CoinManager.Instance == null)
        {
            Debug.LogError("[Item_01] CoinManager.Instance = NULL!");
            return;
        }

        Debug.Log($"[Item_01] Thử mua với giá {price} coin");

        if (CoinManager.Instance.SpendCoin(price))
        {
            Debug.Log("[Item_01] ✓ Mua thành công!");
            isBought = true;
            UpdateButtonUI();
        }
        else
        {
            Debug.LogWarning("[Item_01] ❌ Không đủ tiền!");
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
            Debug.Log("[Item_01] UI: BOUGHT");
        }
        else
        {
            buttonImage.color = normalColor;
            buttonText.text = price.ToString();
            buyButton.interactable = true;
            Debug.Log($"[Item_01] UI: {price} coin");
        }
    }
}

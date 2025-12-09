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

    private void Start()
    {
        Debug.Log("[Item_01] Start() được gọi");

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

        Debug.Log($"[Item_01] Đang thử mua với giá {price} coin...");

        if (CoinManager.Instance.SpendCoin(price))
        {
            Debug.Log("[Item_01] ✓ Đủ tiền!");

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
            Debug.Log("[Item_01] UI cập nhật: BOUGHT");
        }
        else
        {
            buttonImage.color = normalColor;
            buttonText.text = price.ToString();
            buyButton.interactable = true;
            Debug.Log($"[Item_01] UI cập nhật: {price} coin");
        }
    }
}

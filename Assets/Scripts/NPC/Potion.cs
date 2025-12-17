using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Potion : MonoBehaviour
{
    [Header("UI Components")]
    public Button buyButton;
    public TextMeshProUGUI buttonText;     
    public TextMeshProUGUI quantityText;    
    public Canvas canvasOnBuy;              
    public Canvas canvasOnWarning;         

    [Header("Shop Settings")]
    public int price = 20;
    public int quantity = 1;
    public Color availableColor = Color.yellow;
    public Color soldOutColor = Color.gray;

    private void Start()
    {
        UpdateButtonUI();
        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void UpdateButtonUI()
{
    Image buttonImage = buyButton.GetComponent<Image>(); 

    if (quantity > 0)
    {
        buttonText.text = $"{price}";
        if (buttonImage != null)
            buttonImage.color = Color.yellow;    
        quantityText.text = $"Số lượng: {quantity}";
        buyButton.interactable = true;
    }
    else
    {
        buttonText.text = "Hết hàng";
        if (buttonImage != null)
            buttonImage.color = Color.gray;        
        quantityText.text = $"Số lượng: 0";
        buyButton.interactable = false;
    }
}

    private void OnBuyButtonClicked()
    {
        if (quantity <= 0) return;

        if (CoinManager.Instance.SpendCoin(price))
        {
            quantity--;
            UpdateButtonUI();
            if (canvasOnBuy != null)
                canvasOnBuy.gameObject.SetActive(true);
        }
        else
        {
            if (canvasOnWarning != null)
                canvasOnWarning.gameObject.SetActive(true);
        }
    }
}

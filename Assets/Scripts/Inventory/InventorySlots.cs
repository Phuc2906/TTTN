using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public string playerPrefKey;

    public Image iconImage;
    public GameObject iconObject;

    public TextMeshProUGUI equippedText;
    public Button button;

    public bool isEquipped;
    public HotbarSlot equippedHotbarSlot;

    public Image slotImage;

    private Color originalSlotColor = Color.white; 
    public Color equippedColor = new Color(1f, 0.6f, 0f); 

    private static InventorySlot currentSelectedSlot;

    void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (string.IsNullOrEmpty(playerPrefKey)) return;

        if (isEquipped)
        {
            if (equippedHotbarSlot != null)
            {
                equippedHotbarSlot.ForceUnequip();
                equippedHotbarSlot = null;
            }

            SetEquipped(false);
            ClearSelection();
            ItemDragManager.Instance.Clear();
            return;
        }

        if (currentSelectedSlot != null && currentSelectedSlot != this)
        {
            currentSelectedSlot.ClearSelection();
        }

        ItemDragManager.Instance.SelectInventorySlot(this);
        SetSelected(true);
    }

    public void InitEmpty()
    {
        playerPrefKey = "";
        iconImage.sprite = null;
        iconObject.SetActive(false);

        isEquipped = false;
        equippedHotbarSlot = null;
        equippedText.gameObject.SetActive(false);

        ClearSelection();
        ResetColor();
    }

    public void InitItem(string key, Sprite icon)
    {
        playerPrefKey = key;
        iconImage.sprite = icon;
        iconObject.SetActive(true);

        isEquipped = false;
        equippedHotbarSlot = null;
        equippedText.gameObject.SetActive(false);

        ClearSelection();
        ResetColor();
    }

    public void SetEquipped(bool value)
    {
        if (string.IsNullOrEmpty(playerPrefKey))
            return;

        isEquipped = value;
        equippedText.gameObject.SetActive(value);

        if (value)
            ApplyEquippedColor();
        else
            ResetColor();
    }

    void SetSelected(bool value)
    {
        if (slotImage == null || isEquipped) return;

        if (value)
        {
            if (currentSelectedSlot != null && currentSelectedSlot != this)
                currentSelectedSlot.ResetColor();

            currentSelectedSlot = this;
            slotImage.color = Color.yellow;
        }
        else
        {
            ResetColor();
        }
    }

    void ClearSelection()
    {
        if (currentSelectedSlot == this)
            currentSelectedSlot = null;

        ResetColor();
    }

    void ApplyEquippedColor()
    {
        if (slotImage == null) return;

        if (currentSelectedSlot == this)
            currentSelectedSlot = null;

        slotImage.color = equippedColor;
    }

    void ResetColor()
    {
        if (slotImage == null) return;

        if (isEquipped)
            slotImage.color = equippedColor;
        else
            slotImage.color = originalSlotColor; 
    }
}

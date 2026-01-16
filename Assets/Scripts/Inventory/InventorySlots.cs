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
            currentSelectedSlot.ClearSelection();

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

        ResetColor();
    }

    public void SetEquipped(bool value)
    {
        if (string.IsNullOrEmpty(playerPrefKey)) return;

        isEquipped = value;
        equippedText.gameObject.SetActive(value);
        slotImage.color = value ? equippedColor : originalSlotColor;
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
        else ResetColor();
    }

    void ClearSelection()
    {
        if (currentSelectedSlot == this)
            currentSelectedSlot = null;
        ResetColor();
    }

    void ResetColor()
    {
        if (slotImage == null) return;
        slotImage.color = isEquipped ? equippedColor : originalSlotColor;
    }

    public void SyncFromHotbar()
    {
        HotbarSlot[] hotbarSlots = FindObjectsOfType<HotbarSlot>();

        foreach (var slot in hotbarSlots)
        {
            if (!string.IsNullOrEmpty(slot.playerPrefKey) &&
                slot.playerPrefKey == playerPrefKey)
            {
                isEquipped = true;
                equippedHotbarSlot = slot;
                slot.equippedInventorySlot = this;
                SetEquipped(true);
                return;
            }
        }

        SetEquipped(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Image iconImage;
    public GameObject iconObject;
    public Button button;
    public WeaponController weaponController;

    public string playerPrefKey;
    public InventorySlot equippedInventorySlot;

    public Image slotImage;

    private Color originalSlotColor;
    private static HotbarSlot currentSelectedSlot;

    [Header("Save / Load")]
    public int hotbarIndex;

    void Awake()
    {
        button.onClick.AddListener(OnClick);

        iconObject.SetActive(true);
        iconImage.enabled = false;

        if (slotImage != null)
            originalSlotColor = slotImage.color;
    }

    void Start()
    {
        Load();          
        LoadSelected();  
    }

    void OnClick()
    {
        SetSelected(true);
        SaveSelected(); 

        InventorySlot invSlot = ItemDragManager.Instance.selectedInventorySlot;
        if (invSlot != null)
        {
            Assign(invSlot);
            ItemDragManager.Instance.Clear();
            return;
        }

        if (!string.IsNullOrEmpty(playerPrefKey))
            weaponController.EquipWeaponByKey(playerPrefKey);
        else
            weaponController.UnequipWeapon();
    }

    void Assign(InventorySlot invSlot)
    {
        if (equippedInventorySlot != null)
        {
            equippedInventorySlot.SetEquipped(false);
            equippedInventorySlot.equippedHotbarSlot = null;
        }

        playerPrefKey = invSlot.playerPrefKey;
        iconImage.sprite = invSlot.iconImage.sprite;
        iconImage.enabled = true;
        iconObject.SetActive(true);

        equippedInventorySlot = invSlot;
        invSlot.SetEquipped(true);
        invSlot.equippedHotbarSlot = this;

        weaponController.EquipWeaponByKey(playerPrefKey);
        Save();
    }

    public void ForceUnequip()
    {
        playerPrefKey = "";
        iconImage.sprite = null;
        iconImage.enabled = false;

        if (equippedInventorySlot != null)
        {
            equippedInventorySlot.SetEquipped(false);
            equippedInventorySlot.equippedHotbarSlot = null;
        }

        equippedInventorySlot = null;
        weaponController.UnequipWeapon();

        Save();
        ClearSelection();
    }

    void Save()
    {
        string key = "HOTBAR_" + hotbarIndex;

        if (string.IsNullOrEmpty(playerPrefKey))
            PlayerPrefs.DeleteKey(key);
        else
            PlayerPrefs.SetString(key, playerPrefKey);
    }

    void Load()
    {
        string key = "HOTBAR_" + hotbarIndex;
        if (!PlayerPrefs.HasKey(key)) return;

        playerPrefKey = PlayerPrefs.GetString(key);

        Sprite icon = ItemIconDatabase.Instance.GetIcon(playerPrefKey);
        iconImage.sprite = icon;

        iconImage.enabled = true;
        iconObject.SetActive(true);

        weaponController.EquipWeaponByKey(playerPrefKey);
    }

    void SaveSelected()
    {
        PlayerPrefs.SetInt("HOTBAR_SELECTED", hotbarIndex);
    }

    void LoadSelected()
    {
        if (!PlayerPrefs.HasKey("HOTBAR_SELECTED")) return;

        int selectedIndex = PlayerPrefs.GetInt("HOTBAR_SELECTED");
        if (selectedIndex == hotbarIndex)
        {
            SetSelected(true);
        }
    }

    void SetSelected(bool value)
    {
        if (slotImage == null) return;

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

    void ResetColor()
    {
        if (slotImage == null) return;
        slotImage.color = originalSlotColor;
    }
}

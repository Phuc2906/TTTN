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

    void Awake()
    {
        button.onClick.AddListener(OnClick);

        iconObject.SetActive(true);
        iconImage.enabled = false;

        if (slotImage != null)
            originalSlotColor = slotImage.color;
    }

    void OnClick()
{
    SetSelected(true);

    InventorySlot invSlot = ItemDragManager.Instance.selectedInventorySlot;

    if (invSlot != null)
    {
        Assign(invSlot);
        ItemDragManager.Instance.Clear();
    }
    else
    {
        if (!string.IsNullOrEmpty(playerPrefKey))
        {
            weaponController.EquipWeaponByKey(playerPrefKey);
        }
    }
}


    void Assign(InventorySlot invSlot)
{
    if (equippedInventorySlot != null)
    {
        equippedInventorySlot.isEquipped = false;
        equippedInventorySlot.equippedHotbarSlot = null;
        equippedInventorySlot.SetEquipped(false);
    }

    playerPrefKey = invSlot.playerPrefKey;
    iconImage.sprite = invSlot.iconImage.sprite;

    iconImage.enabled = true;
    iconObject.SetActive(true);

    equippedInventorySlot = invSlot;

    invSlot.SetEquipped(true);
    invSlot.equippedHotbarSlot = this;

    weaponController.EquipWeaponByKey(playerPrefKey);
}


   public void ForceUnequip()
{
    playerPrefKey = "";
    iconImage.sprite = null;
    iconImage.enabled = false;

    equippedInventorySlot = null;

    
    weaponController.UnequipWeapon();

    ClearSelection();
}


    void SetSelected(bool value)
    {
        if (slotImage == null) return;

        if (value)
        {
            if (currentSelectedSlot != null && currentSelectedSlot != this)
            {
                currentSelectedSlot.ResetColor();
            }

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
using UnityEngine;

public class ItemDragManager : MonoBehaviour
{
    public static ItemDragManager Instance;
    public InventorySlot selectedInventorySlot;

    void Awake()
    {
        Instance = this;
    }

    public void SelectInventorySlot(InventorySlot slot)
    {
        selectedInventorySlot = slot;
    }

    public void Clear()
    {
        selectedInventorySlot = null;
    }
}

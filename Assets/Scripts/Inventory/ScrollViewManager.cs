using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    public Transform content;
    public GameObject slotPrefab;

    [Header("Inventory Config")]
    public int totalSlots = 20;

    [Header("Item Data")]
    public string[] playerPrefKeys;
    public Sprite[] itemIcons;

    void Start()
    {
        SpawnSlots();
    }

    void SpawnSlots()
    {
        int itemCount = Mathf.Min(playerPrefKeys.Length, itemIcons.Length);

        for (int i = 0; i < totalSlots; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, content);
            InventorySlot slot = slotGO.GetComponent<InventorySlot>();

            if (i < itemCount)
            {
                slot.InitItem(playerPrefKeys[i], itemIcons[i]);
            }
            else
            {
                slot.InitEmpty();
            }
        }
    }
}
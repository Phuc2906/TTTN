using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    public Transform content;
    public GameObject slotPrefab;

    public int totalSlots = 20;

    public string[] playerPrefKeys;
    public Sprite[] itemIcons;

    void Start()
    {
        SpawnSlots();
    }

    public void Refresh()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        SpawnSlots();
    }

    void SpawnSlots()
    {
        int slotIndex = 0;

        for (int i = 0; i < playerPrefKeys.Length && slotIndex < totalSlots; i++)
        {
            string key = playerPrefKeys[i];

            if (PlayerPrefs.HasKey(key))
            {
                GameObject slotGO = Instantiate(slotPrefab, content);
                InventorySlot slot = slotGO.GetComponent<InventorySlot>();

                slot.InitItem(key, itemIcons[i]);
                slot.SyncFromHotbar();

                slotIndex++;
            }
        }

        while (slotIndex < totalSlots)
        {
            GameObject slotGO = Instantiate(slotPrefab, content);
            InventorySlot slot = slotGO.GetComponent<InventorySlot>();

            slot.InitEmpty();
            slot.SyncFromHotbar();

            slotIndex++;
        }
    }
}
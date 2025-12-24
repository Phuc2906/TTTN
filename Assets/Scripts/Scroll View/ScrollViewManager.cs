using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    public Transform content;     
    public GameObject itemPrefab;  

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject item = Instantiate(itemPrefab, content);
            item.name = "Item " + i;
        }
    }
}

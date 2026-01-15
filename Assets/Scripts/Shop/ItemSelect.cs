using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    public string itemKey;

    [Header("UI")]
    public GameObject canvasToClose;

    public void OnClickAddItem()
    {
        PlayerPrefs.SetInt(itemKey, 1);
        PlayerPrefs.Save();

        if (canvasToClose != null)
            canvasToClose.SetActive(false);
    }
}

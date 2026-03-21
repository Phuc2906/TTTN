using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    public string itemKey;

    public GameObject canvasToClose;

    public ScrollViewManager scrollViewManager; 

    public void OnClickAddItem()
    {
        PlayerPrefs.SetInt(itemKey, 1);
        PlayerPrefs.Save();

        
        if (scrollViewManager != null)
            scrollViewManager.Refresh();

        if (canvasToClose != null)
            canvasToClose.SetActive(false);
    }
}
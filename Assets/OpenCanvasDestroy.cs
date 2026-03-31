using UnityEngine;

public class OpenCanvasDestroy : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject targetCanvas;
    public GameObject closeCanvas;

    [Header("NPC")]
    public GameObject npcObject;

    [Header("PlayerPrefs Key")]
    public string npcKey = "NPC_Answer_02";

    void Start()
    {
        if (PlayerPrefs.GetInt(npcKey, 0) == 1)
        {
            if (npcObject != null)
                Destroy(npcObject);
        }
    }

    public void OnClickNPC()
    {
        if (targetCanvas != null)
            targetCanvas.SetActive(true);
        if (closeCanvas != null)
            closeCanvas.SetActive(false);
            
        PlayerPrefs.SetInt(npcKey, 1);
        PlayerPrefs.Save();

        if (npcObject != null)
            Destroy(npcObject);
    }
}
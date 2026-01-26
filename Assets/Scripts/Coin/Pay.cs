using UnityEngine;

public class Pay : MonoBehaviour
{
    [Header("Save")]
    public string rentID = "Rent_NPC_01";

    [Header("Cost")]
    public int cost = 30;

    [Header("Objects To Destroy")]
    public GameObject objectToDestroy;   
    public GameObject textToDestroy;     

    [Header("Canvas")]
    public GameObject communicateCanvas;
    public GameObject notEnoughCanvas;

    void Start()
    {
        if (PlayerPrefs.GetInt(rentID, 0) == 1)
        {
            DestroySavedObjects();
        }
    }

    public void OnButtonClick()
    {
        if (CoinManager.Instance == null) return;

        if (CoinManager.Instance.SpendCoin(cost))
        {
            PlayerPrefs.SetInt(rentID, 1);
            PlayerPrefs.Save();

            DestroySavedObjects();

            if (communicateCanvas != null)
                communicateCanvas.SetActive(false);
        }
        else
        {
            if (notEnoughCanvas != null)
                notEnoughCanvas.SetActive(true);
        }
    }

    void DestroySavedObjects()
    {
        if (objectToDestroy != null)
            Destroy(objectToDestroy);

        if (textToDestroy != null)
            Destroy(textToDestroy);
    }
}

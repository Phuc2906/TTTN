using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CanvasGroupData
{
    public GameObject canvas;
    public Button wrongButton;
}

public class PlayCountManager : MonoBehaviour
{
    public static PlayCountManager Instance;

    [Header("Số lượt")]
    public int playCount = 3;

    [Header("Text")]
    public TMP_Text[] playCountTexts;

    [Header("Danh sách canvas")]
    public CanvasGroupData[] canvasGroups;

    [Header("Popup sai")]
    public GameObject wrongPopup;

    [Header("Canvas cuối")]
    public GameObject finalCanvas;

    [Header("NPC")]
    public GameObject npcObject;
    public string npcKey = "NPC_Answer";

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateAllText();

        if (PlayerPrefs.GetInt(npcKey, 0) == 1)
        {
            if (npcObject != null)
                Destroy(npcObject);
        }

        for (int i = 0; i < canvasGroups.Length; i++)
        {
            if (canvasGroups[i].canvas != null)
                canvasGroups[i].canvas.SetActive(false);
        }
    }

    public void OnWrongClick(int index)
    {
        if (wrongPopup != null)
            wrongPopup.SetActive(true);

        if (playCount > 0)
        {
            playCount--;
            UpdateAllText();
        }

        if (playCount <= 0)
        {
            if (canvasGroups[index].canvas != null)
                canvasGroups[index].canvas.SetActive(false);

            PlayerPrefs.SetInt(npcKey, 1);
            PlayerPrefs.Save();

            if (npcObject != null)
                Destroy(npcObject);

            return;
        }

        if (canvasGroups[index].canvas != null)
            canvasGroups[index].canvas.SetActive(false);

        if (index == canvasGroups.Length - 1)
        {
            if (finalCanvas != null)
                finalCanvas.SetActive(true);
        }
        else
        {
            if (canvasGroups[index + 1].canvas != null)
                canvasGroups[index + 1].canvas.SetActive(true);
        }
    }

    void UpdateAllText()
    {
        foreach (TMP_Text txt in playCountTexts)
        {
            if (txt != null)
                txt.text = "Số lượt trả lời: " + playCount;
        }
    }

    public void SetCount(int value)
    {
        playCount = value;
        UpdateAllText();
    }
}
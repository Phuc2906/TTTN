using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public CanvasGroup easyGroup;
    public CanvasGroup normalGroup;
    public CanvasGroup hardGroup;

    private string modeKey = "GameMode";

    void Start()
    {
        UpdateUI();
    }

    public void SelectEasy()
    {
        if (PlayerPrefs.GetInt(modeKey, 0) == 0)
        {
            PlayerPrefs.SetInt(modeKey, 1);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    public void SelectNormal()
    {
        if (PlayerPrefs.GetInt(modeKey, 0) == 0)
        {
            PlayerPrefs.SetInt(modeKey, 2);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    public void SelectHard()
    {
        if (PlayerPrefs.GetInt(modeKey, 0) == 0)
        {
            PlayerPrefs.SetInt(modeKey, 3);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        int mode = PlayerPrefs.GetInt(modeKey, 0);

        if (mode == 0)
        {
            SetGroup(easyGroup, true);
            SetGroup(normalGroup, true);
            SetGroup(hardGroup, true);
            return;
        }

        SetGroup(easyGroup, mode == 1);
        SetGroup(normalGroup, mode == 2);
        SetGroup(hardGroup, mode == 3);
    }

    void SetGroup(CanvasGroup group, bool active)
    {
        group.interactable = active;
        group.blocksRaycasts = active;
        group.alpha = active ? 1f : 0.5f;
    }
    
    public void Refresh()
    {
        UpdateUI();
    }
}